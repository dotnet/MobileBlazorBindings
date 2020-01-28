// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ButtonHandler : ViewHandler
    {
        public ButtonHandler(NativeComponentRenderer renderer, XF.Button buttonControl) : base(renderer, buttonControl)
        {
            ButtonControl = buttonControl ?? throw new ArgumentNullException(nameof(buttonControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Button ButtonControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (attributeEventHandlerId != 0)
            {
                ApplyEventHandlerId(attributeName, attributeEventHandlerId);
            }

            switch (attributeName)
            {
                case nameof(Button.Text):
                    ButtonControl.Text = (string)attributeValue;
                    break;
                case nameof(Button.TextColor):
                    ButtonControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        partial void ApplyEventHandlerId(string attributeName, ulong attributeEventHandlerId);
    }
}
