// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class BaseShellItemHandler : NavigableElementHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            RegisterEvent(
                eventName: "onappearing",
                setId: id => AppearingEventHandlerId = id,
                clearId: id => { if (AppearingEventHandlerId == id) { AppearingEventHandlerId = 0; } });
            BaseShellItemControl.Appearing += (s, e) =>
            {
                if (AppearingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(AppearingEventHandlerId, null, e));
                }
            };
            RegisterEvent(
                eventName: "ondisappearing",
                setId: id => DisappearingEventHandlerId = id,
                clearId: id => { if (DisappearingEventHandlerId == id) { DisappearingEventHandlerId = 0; } });
            BaseShellItemControl.Disappearing += (s, e) =>
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
