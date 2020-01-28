// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ButtonHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            RegisterEvent(
                eventName: "onclick",
                setId: id => ClickEventHandlerId = id,
                clearId: () => ClickEventHandlerId = 0);
            ButtonControl.Clicked += (s, e) =>
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
