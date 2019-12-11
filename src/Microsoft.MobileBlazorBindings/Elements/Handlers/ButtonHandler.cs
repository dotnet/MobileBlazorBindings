// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ButtonHandler : ViewHandler
    {
        public ButtonHandler(NativeComponentRenderer renderer, XF.Button buttonControl) : base(renderer, buttonControl)
        {
            ButtonControl = buttonControl ?? throw new ArgumentNullException(nameof(buttonControl));
            ButtonControl.Clicked += (s, e) =>
            {
                if (ClickEventHandlerId != default)
                {
                    renderer.Dispatcher.InvokeAsync(() => renderer.DispatchEventAsync(ClickEventHandlerId, null, e));
                }
            };
        }

        public XF.Button ButtonControl { get; }
        public ulong ClickEventHandlerId { get; set; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(Button.Text):
                    ButtonControl.Text = (string)attributeValue;
                    break;
                case nameof(Button.TextColor):
                    ButtonControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case "onclick":
                    Renderer.RegisterEvent(attributeEventHandlerId, () => ClickEventHandlerId = 0);
                    ClickEventHandlerId = attributeEventHandlerId;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
