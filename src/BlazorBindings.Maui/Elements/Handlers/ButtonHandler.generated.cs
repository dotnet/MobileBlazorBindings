// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ButtonHandler : ViewHandler
    {
        private static readonly Color BorderColorDefaultValue = MC.Button.BorderColorProperty.DefaultValue is Color value ? value : default;
        private static readonly double BorderWidthDefaultValue = MC.Button.BorderWidthProperty.DefaultValue is double value ? value : default;
        private static readonly double CharacterSpacingDefaultValue = MC.Button.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly int CornerRadiusDefaultValue = MC.Button.CornerRadiusProperty.DefaultValue is int value ? value : default;
        private static readonly MC.FontAttributes FontAttributesDefaultValue = MC.Button.FontAttributesProperty.DefaultValue is MC.FontAttributes value ? value : default;
        private static readonly bool FontAutoScalingEnabledDefaultValue = MC.Button.FontAutoScalingEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string FontFamilyDefaultValue = MC.Button.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = MC.Button.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly MC.ImageSource ImageSourceDefaultValue = MC.Button.ImageSourceProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly LineBreakMode LineBreakModeDefaultValue = MC.Button.LineBreakModeProperty.DefaultValue is LineBreakMode value ? value : default;
        private static readonly Thickness PaddingDefaultValue = MC.Button.PaddingProperty.DefaultValue is Thickness value ? value : default;
        private static readonly string TextDefaultValue = MC.Button.TextProperty.DefaultValue is string value ? value : default;
        private static readonly Color TextColorDefaultValue = MC.Button.TextColorProperty.DefaultValue is Color value ? value : default;
        private static readonly TextTransform TextTransformDefaultValue = MC.Button.TextTransformProperty.DefaultValue is TextTransform value ? value : default;

        public ButtonHandler(NativeComponentRenderer renderer, MC.Button buttonControl) : base(renderer, buttonControl)
        {
            ButtonControl = buttonControl ?? throw new ArgumentNullException(nameof(buttonControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Button ButtonControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Button.BorderColor):
                    ButtonControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue, BorderColorDefaultValue);
                    break;
                case nameof(MC.Button.BorderWidth):
                    ButtonControl.BorderWidth = AttributeHelper.StringToDouble((string)attributeValue, BorderWidthDefaultValue);
                    break;
                case nameof(MC.Button.CharacterSpacing):
                    ButtonControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(MC.Button.CornerRadius):
                    ButtonControl.CornerRadius = AttributeHelper.GetInt(attributeValue, CornerRadiusDefaultValue);
                    break;
                case nameof(MC.Button.FontAttributes):
                    ButtonControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(MC.Button.FontAutoScalingEnabled):
                    ButtonControl.FontAutoScalingEnabled = AttributeHelper.GetBool(attributeValue, FontAutoScalingEnabledDefaultValue);
                    break;
                case nameof(MC.Button.FontFamily):
                    ButtonControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(MC.Button.FontSize):
                    ButtonControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(MC.Button.ImageSource):
                    ButtonControl.ImageSource = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, ImageSourceDefaultValue);
                    break;
                case nameof(MC.Button.LineBreakMode):
                    ButtonControl.LineBreakMode = (LineBreakMode)AttributeHelper.GetInt(attributeValue, (int)LineBreakModeDefaultValue);
                    break;
                case nameof(MC.Button.Padding):
                    ButtonControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                case nameof(MC.Button.Text):
                    ButtonControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                case nameof(MC.Button.TextColor):
                    ButtonControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(MC.Button.TextTransform):
                    ButtonControl.TextTransform = (TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
