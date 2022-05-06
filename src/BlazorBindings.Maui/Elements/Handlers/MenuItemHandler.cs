// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class MenuItemHandler : BaseMenuItemHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onclick",
                setId: id => ClickEventHandlerId = id,
                clearId: id => { if (ClickEventHandlerId == id) { ClickEventHandlerId = 0; } });
            MenuItemControl.Clicked += (s, e) =>
            {
                if (ClickEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, e));
                }
            };
        }

        public ulong ClickEventHandlerId { get; set; }
    }
}
