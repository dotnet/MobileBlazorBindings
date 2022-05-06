// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ShellHandler : PageHandler, IMauiContainerElementHandler
    {
        private readonly MC.ContentView _flyoutHeaderContentView = new MC.ContentView();

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

        public virtual void AddChild(MC.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            MC.ShellItem itemToAdd = child switch
            {
                MC.TemplatedPage childAsTemplatedPage => childAsTemplatedPage, // Implicit conversion
                MC.ShellContent childAsShellContent => childAsShellContent, // Implicit conversion
                MC.ShellSection childAsShellSection => childAsShellSection, // Implicit conversion
                MC.MenuItem childAsMenuItem => childAsMenuItem, // Implicit conversion
                MC.ShellItem childAsShellItem => childAsShellItem,
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

        public virtual void RemoveChild(MC.Element child)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            var itemToRemove = GetItemForElement(child)
                ?? throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support removing a child (child type is '{child.GetType().FullName}').");

            ShellControl.Items.Remove(itemToRemove);
        }

        public int GetChildIndex(MC.Element child)
        {
            var shellItem = GetItemForElement(child);
            return ShellControl.Items.IndexOf(shellItem);
        }

        private MC.ShellItem GetItemForElement(MC.Element child)
        {
            return child switch
            {
                MC.TemplatedPage childAsTemplatedPage => GetItemForTemplatedPage(childAsTemplatedPage),
                MC.ShellContent childAsShellContent => GetItemForContent(childAsShellContent),
                MC.ShellSection childAsShellSection => GetItemForSection(childAsShellSection),
                MC.MenuItem childAsMenuItem => GetItemForMenuItem(childAsMenuItem),
                MC.ShellItem childAsShellItem => childAsShellItem,
                _ => null
            };
        }

        private MC.ShellItem GetItemForTemplatedPage(MC.TemplatedPage childAsTemplatedPage)
        {
            return ShellControl.Items
                .FirstOrDefault(item => item.Items
                    .Any(section => section.Items.Any(content => content.Content == childAsTemplatedPage)));
        }

        private MC.ShellItem GetItemForContent(MC.ShellContent childAsShellContent)
        {
            return ShellControl.Items
                .FirstOrDefault(item => item.Items
                    .Any(section => section.Items.Contains(childAsShellContent)));
        }

        private MC.ShellItem GetItemForSection(MC.ShellSection childAsShellSection)
        {
            return ShellControl.Items.FirstOrDefault(item => item.Items.Contains(childAsShellSection));
        }

        private MC.ShellItem GetItemForMenuItem(MC.MenuItem childAsMenuItem)
        {
            // MenuItem is wrapped in ShellMenuItem, which is internal type.
            // Not sure how to identify this item correctly.
            return ShellControl.Items.FirstOrDefault(item => IsShellItemWithMenuItem(item, childAsMenuItem));
        }

        private static bool IsShellItemWithMenuItem(MC.ShellItem shellItem, MC.MenuItem menuItem)
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

        private static readonly Type MenuShellItemType = typeof(MC.ShellItem).Assembly.GetType("Microsoft.Maui.Controls.MenuShellItem");
        private static readonly PropertyInfo MenuShellItemMenuItemProperty = MenuShellItemType.GetProperty("MenuItem");
    }
}
