// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ButtonHandler : ViewHandler
    {
        private static readonly XF.Color BorderColorDefaultValue = XF.Button.BorderColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly double BorderWidthDefaultValue = XF.Button.BorderWidthProperty.DefaultValue is double value ? value : default;
        private static readonly double CharacterSpacingDefaultValue = XF.Button.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly int CornerRadiusDefaultValue = XF.Button.CornerRadiusProperty.DefaultValue is int value ? value : default;
        private static readonly XF.FontAttributes FontAttributesDefaultValue = XF.Button.FontAttributesProperty.DefaultValue is XF.FontAttributes value ? value : default;
        private static readonly string FontFamilyDefaultValue = XF.Button.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = XF.Button.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly XF.ImageSource ImageSourceDefaultValue = XF.Button.ImageSourceProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly XF.Thickness PaddingDefaultValue = XF.Button.PaddingProperty.DefaultValue is XF.Thickness value ? value : default;
        private static readonly string TextDefaultValue = XF.Button.TextProperty.DefaultValue is string value ? value : default;
        private static readonly XF.Color TextColorDefaultValue = XF.Button.TextColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.TextTransform TextTransformDefaultValue = XF.Button.TextTransformProperty.DefaultValue is XF.TextTransform value ? value : default;

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
                    ButtonControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue, BorderColorDefaultValue);
                    break;
                case nameof(XF.Button.BorderWidth):
                    ButtonControl.BorderWidth = AttributeHelper.StringToDouble((string)attributeValue, BorderWidthDefaultValue);
                    break;
                case nameof(XF.Button.CharacterSpacing):
                    ButtonControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(XF.Button.CornerRadius):
                    ButtonControl.CornerRadius = AttributeHelper.GetInt(attributeValue, CornerRadiusDefaultValue);
                    break;
                case nameof(XF.Button.FontAttributes):
                    ButtonControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(XF.Button.FontFamily):
                    ButtonControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(XF.Button.FontSize):
                    ButtonControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(XF.Button.ImageSource):
                    ButtonControl.ImageSource = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, ImageSourceDefaultValue);
                    break;
                case nameof(XF.Button.Padding):
                    ButtonControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                case nameof(XF.Button.Text):
                    ButtonControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                case nameof(XF.Button.TextColor):
                    ButtonControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(XF.Button.TextTransform):
                    ButtonControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
