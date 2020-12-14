// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class PageHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ConfigureEvent(
                eventName: "onappearing",
                setId: id => AppearingEventHandlerId = id,
                clearId: id => { if (AppearingEventHandlerId == id) { AppearingEventHandlerId = 0; } });
            PageControl.Appearing += (s, e) =>
            {
                if (AppearingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(AppearingEventHandlerId, null, e));
                }
            };

            ConfigureEvent(
                eventName: "ondisappearing",
                setId: id => DisappearingEventHandlerId = id,
                clearId: id => { if (DisappearingEventHandlerId == id) { DisappearingEventHandlerId = 0; } });
            PageControl.Disappearing += (s, e) =>
            {
                if (DisappearingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(DisappearingEventHandlerId, null, e));
                }
            };
        }

        public ulong AppearingEventHandlerId { get; set; }
        public ulong DisappearingEventHandlerId { get; set; }
    }
}
