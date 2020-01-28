// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class BaseShellItemHandler : NavigableElementHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            BaseShellItemControl.Appearing += (s, e) =>
            {
                if (AppearingEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(AppearingEventHandlerId, null, e));
                }
            };
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

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId)
        {
            switch (attributeName)
            {
                case "onappearing":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => AppearingEventHandlerId = 0);
                    AppearingEventHandlerId = attributeEventHandlerId;
                    break;
                case "ondisappearing":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => DisappearingEventHandlerId = 0);
                    DisappearingEventHandlerId = attributeEventHandlerId;
                    break;
            }
        }
    }
}
