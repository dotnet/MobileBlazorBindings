// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ButtonHandler : ViewHandler
    {
        partial void Initialize(NativeComponentRenderer renderer)
        {
            ButtonControl.Clicked += (s, e) =>
            {
                if (ClickEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, e));
                }
            };
        }

        public ulong ClickEventHandlerId { get; set; }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId)
        {
            switch (attributeName)
            {
                case "onclick":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => ClickEventHandlerId = 0);
                    ClickEventHandlerId = attributeEventHandlerId;
                    break;
            }
        }
    }
}
