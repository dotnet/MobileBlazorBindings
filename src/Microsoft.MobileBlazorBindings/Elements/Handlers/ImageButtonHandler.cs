// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ImageButtonHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            RegisterEvent(
                eventName: "onclick",
                setId: id => ClickEventHandlerId = id,
                clearId: id => { if (ClickEventHandlerId == id) { ClickEventHandlerId = 0; } });
            ImageButtonControl.Clicked += (s, e) =>
            {
                if (ClickEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, e));
                }
            };

            RegisterEvent(
                eventName: "onpress",
                setId: id => PressEventHandlerId = id,
                clearId: id => { if (PressEventHandlerId == id) { PressEventHandlerId = 0; } });
            ImageButtonControl.Pressed += (s, e) =>
            {
                if (PressEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(PressEventHandlerId, null, e));
                }
            };

            RegisterEvent(
                eventName: "onrelease",
                setId: id => ReleaseEventHandlerId = id,
                clearId: id => { if (ReleaseEventHandlerId == id) { ReleaseEventHandlerId = 0; } });
            ImageButtonControl.Released += (s, e) =>
            {
                if (ReleaseEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ReleaseEventHandlerId, null, e));
                }
            };
        }

        public ulong ClickEventHandlerId { get; set; }
        public ulong PressEventHandlerId { get; set; }
        public ulong ReleaseEventHandlerId { get; set; }
    }
}
