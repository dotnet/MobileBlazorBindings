// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class MenuItemHandler : BaseMenuItemHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            RegisterEvent(
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
