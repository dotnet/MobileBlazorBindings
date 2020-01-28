﻿// Copyright (c) Microsoft Corporation.
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

            ShellControl.Navigated += (s, e) =>
            {
                if (NavigatedEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(NavigatedEventHandlerId, null, e));
                }
            };
            ShellControl.Navigating += (s, e) =>
            {
                if (NavigatingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(NavigatingEventHandlerId, null, e));
                }
            };
        }

        internal bool ClearDummyChild()
        {
            // Remove the dummy ShellContent if it's still there. This won't throw even if the item is already removed.
            return ShellControl.Items.Remove(_dummyShellContent);
        }

        public ulong NavigatedEventHandlerId { get; set; }
        public ulong NavigatingEventHandlerId { get; set; }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId)
        {
            switch (attributeName)
            {
                case "onnavigated":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => NavigatedEventHandlerId = 0);
                    NavigatedEventHandlerId = attributeEventHandlerId;
                    break;
                case "onnavigating":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => NavigatingEventHandlerId = 0);
                    NavigatingEventHandlerId = attributeEventHandlerId;
                    break;
            }
        }
    }
}
