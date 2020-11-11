// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellHandler : PageHandler, IXamarinFormsContainerElementHandler
    {
        private readonly XF.ContentView _flyoutHeaderContentView = new XF.ContentView();

        partial void Initialize(NativeComponentRenderer renderer)
        {
            // Add a dummy FlyoutHeader because it cannot be set dynamically later. When app code sets
            // its own FlyoutHeader, it will be set as the Content of this ContentView.
            // See https://github.com/xamarin/Xamarin.Forms/issues/6161 ([Bug] Changing the Shell Flyout Header after it's already rendered doesn't work)
            _flyoutHeaderContentView.IsVisible = false;
            ShellControl.FlyoutHeader = _flyoutHeaderContentView;

            ConfigureEvent(
                eventName: "onnavigated",
                setId: id => NavigatedEventHandlerId = id,
                clearId: id => { if (NavigatedEventHandlerId == id) { NavigatedEventHandlerId = 0; } });
            ShellControl.Navigated += (s, e) =>
            {
                if (NavigatedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(NavigatedEventHandlerId, null, e));
                }
            };
            ConfigureEvent(
                eventName: "onnavigating",
                setId: id => NavigatingEventHandlerId = id,
                clearId: id => { if (NavigatingEventHandlerId == id) { NavigatingEventHandlerId = 0; } });
            ShellControl.Navigating += (s, e) =>
            {
                if (NavigatingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(NavigatingEventHandlerId, null, e));
                }
            };
        }

        public ulong NavigatedEventHandlerId { get; set; }
        public ulong NavigatingEventHandlerId { get; set; }

        public virtual void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            XF.ShellItem itemToAdd = child switch
            {
                XF.TemplatedPage childAsTemplatedPage => childAsTemplatedPage, // Implicit conversion
                XF.ShellContent childAsShellContent => childAsShellContent, // Implicit conversion
                XF.ShellSection childAsShellSection => childAsShellSection, // Implicit conversion
                XF.MenuItem childAsMenuItem => childAsMenuItem, // Implicit conversion
                XF.ShellItem childAsShellItem => childAsShellItem,
                _ => throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support adding a child (child type is '{child.GetType().FullName}').")
            };

            if (ShellControl.Items.Count >= physicalSiblingIndex)
            {
                ShellControl.Items.Insert(physicalSiblingIndex, itemToAdd);
            }
            else
            {
                Debug.WriteLine($"WARNING: {nameof(AddChild)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but ShellControl.Items.Count={ShellControl.Items.Count}");
                ShellControl.Items.Add(itemToAdd);
            }
        }

        public virtual void RemoveChild(XF.Element child)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            var itemToRemove = GetItemForElement(child)
                ?? throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support removing a child (child type is '{child.GetType().FullName}').");

            ShellControl.Items.Remove(itemToRemove);
        }

        public int GetChildIndex(XF.Element child)
        {
            var shellItem = GetItemForElement(child);
            return ShellControl.Items.IndexOf(shellItem);
        }

        private XF.ShellItem GetItemForElement(XF.Element child)
        {
            return child switch
            {
                XF.TemplatedPage childAsTemplatedPage => GetItemForTemplatedPage(childAsTemplatedPage),
                XF.ShellContent childAsShellContent => GetItemForContent(childAsShellContent),
                XF.ShellSection childAsShellSection => GetItemForSection(childAsShellSection),
                XF.MenuItem childAsMenuItem => GetItemForMenuItem(childAsMenuItem),
                XF.ShellItem childAsShellItem => childAsShellItem,
                _ => null
            };
        }

        private XF.ShellItem GetItemForTemplatedPage(XF.TemplatedPage childAsTemplatedPage)
        {
            return ShellControl.Items
                .FirstOrDefault(item => item.Items
                    .Any(section => section.Items.Any(content => content.Content == childAsTemplatedPage)));
        }

        private XF.ShellItem GetItemForContent(XF.ShellContent childAsShellContent)
        {
            return ShellControl.Items
                .FirstOrDefault(item => item.Items
                    .Any(section => section.Items.Contains(childAsShellContent)));
        }

        private XF.ShellItem GetItemForSection(XF.ShellSection childAsShellSection)
        {
            return ShellControl.Items.FirstOrDefault(item => item.Items.Contains(childAsShellSection));
        }

        private XF.ShellItem GetItemForMenuItem(XF.MenuItem childAsMenuItem)
        {
            // MenuItem is wrapped in ShellMenuItem, which is internal type.
            // Not sure how to identify this item correctly.
            return ShellControl.Items.FirstOrDefault(item => IsShellItemWithMenuItem(item, childAsMenuItem));
        }

        private static bool IsShellItemWithMenuItem(XF.ShellItem shellItem, XF.MenuItem menuItem)
        {
            // Xamarin.Forms.MenuShellItem is internal so we have to use reflection to check that
            // its MenuItem property is the same as the MenuItem we're looking for.
            if (!MenuShellItemType.IsAssignableFrom(shellItem.GetType()))
            {
                return false;
            }
            var menuItemInMenuShellItem = MenuShellItemMenuItemProperty.GetValue(shellItem);
            return menuItemInMenuShellItem == menuItem;
        }

        private static readonly Type MenuShellItemType = typeof(XF.ShellItem).Assembly.GetType("Xamarin.Forms.MenuShellItem");
        private static readonly PropertyInfo MenuShellItemMenuItemProperty = MenuShellItemType.GetProperty("MenuItem");
    }
}
