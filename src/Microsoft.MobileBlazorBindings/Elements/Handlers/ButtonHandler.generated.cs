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
            switch (attributeName)
            {
                case nameof(XF.Button.BorderColor):
                    ButtonControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Button.BorderWidth):
                    ButtonControl.BorderWidth = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.Button.CharacterSpacing):
                    ButtonControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.Button.CornerRadius):
                    ButtonControl.CornerRadius = AttributeHelper.GetInt(attributeValue, -1);
                    break;
                case nameof(XF.Button.FontAttributes):
                    ButtonControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Button.FontFamily):
                    ButtonControl.FontFamily = (string)attributeValue;
                    break;
                case nameof(XF.Button.FontSize):
                    ButtonControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.Button.ImageSource):
                    ButtonControl.ImageSource = AttributeHelper.DelegateToImageSource(attributeValue);
                    break;
                case nameof(XF.Button.Padding):
                    ButtonControl.Padding = AttributeHelper.StringToThickness(attributeValue);
                    break;
                case nameof(XF.Button.Text):
                    ButtonControl.Text = (string)attributeValue;
                    break;
                case nameof(XF.Button.TextColor):
                    ButtonControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Button.TextTransform):
                    ButtonControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)XF.TextTransform.Default);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
