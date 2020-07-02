// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ShellHandler : PageHandler
    {
        private readonly ShellContentMarkerItem _dummyShellContent = new ShellContentMarkerItem();
        private readonly XF.ContentView _flyoutHeaderContentView = new XF.ContentView();

        partial void Initialize(NativeComponentRenderer renderer)
        {
            // Add one item for Shell to load correctly. It will later be removed when the first real
            // item is added by the app.
            ShellControl.Items.Add(_dummyShellContent);

            // Add a dummy FlyoutHeader because it cannot be set dynamically later. When app code sets
            // its own FlyoutHeader, it will be set as the Content of this ContentView.
            // See https://github.com/xamarin/Xamarin.Forms/issues/6161 ([Bug] Changing the Shell Flyout Header after it's already rendered doesn't work)
            _flyoutHeaderContentView.IsVisible = false;
            ShellControl.FlyoutHeader = _flyoutHeaderContentView;

            RegisterEvent(
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
            RegisterEvent(
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

        private bool ClearDummyChild()
        {
            // Remove the dummy ShellContent if it's still there. This won't throw even if the item is already removed.
            return ShellControl.Items.Remove(_dummyShellContent);
        }

        public ulong NavigatedEventHandlerId { get; set; }
        public ulong NavigatingEventHandlerId { get; set; }

        public override void AddChild(XF.Element child, int physicalSiblingIndex)
        {
            if (child is null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            var removedDummyChild = ClearDummyChild();
            switch (child)
            {
                case XF.TemplatedPage childAsTemplatedPage:
                    ShellControl.Items.Add(childAsTemplatedPage); // Implicit conversion
                    break;
                case XF.ShellContent childAsShellContent:
                    ShellControl.Items.Add(childAsShellContent); // Implicit conversion
                    break;
                case XF.ShellSection childAsShellSection:
                    ShellControl.Items.Add(childAsShellSection); // Implicit conversion
                    break;
                case XF.MenuItem childAsMenuItem:
                    ShellControl.Items.Add(childAsMenuItem); // Implicit conversion
                    break;
                case XF.ShellItem childAsShellItem:
                    ShellControl.Items.Add(childAsShellItem);
                    break;
                default:
                    throw new NotSupportedException($"Handler of type '{GetType().FullName}' representing element type '{TargetElement?.GetType().FullName ?? "<null>"}' doesn't support adding a child (child type is '{child.GetType().FullName}').");
            }
            // TODO: If this was the first item added, mark it as the current item
            // But this code seems to cause a NullRef...
            //if (removedDummyChild)
            //{
            //    ShellControl.CurrentItem = parentAsShell.Items[0];
            //}       
        }
    }
}
