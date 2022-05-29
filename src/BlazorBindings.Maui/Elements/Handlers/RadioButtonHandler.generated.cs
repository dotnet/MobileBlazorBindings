// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Core;
using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class RadioButtonHandler : TemplatedViewHandler
    {
        private static readonly Color BorderColorDefaultValue = MC.RadioButton.BorderColorProperty.DefaultValue is Color value ? value : default;
        private static readonly double BorderWidthDefaultValue = MC.RadioButton.BorderWidthProperty.DefaultValue is double value ? value : default;
        private static readonly double CharacterSpacingDefaultValue = MC.RadioButton.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly int CornerRadiusDefaultValue = MC.RadioButton.CornerRadiusProperty.DefaultValue is int value ? value : default;
        private static readonly MC.FontAttributes FontAttributesDefaultValue = MC.RadioButton.FontAttributesProperty.DefaultValue is MC.FontAttributes value ? value : default;
        private static readonly bool FontAutoScalingEnabledDefaultValue = MC.RadioButton.FontAutoScalingEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string FontFamilyDefaultValue = MC.RadioButton.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = MC.RadioButton.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly string GroupNameDefaultValue = MC.RadioButton.GroupNameProperty.DefaultValue is string value ? value : default;
        private static readonly bool IsCheckedDefaultValue = MC.RadioButton.IsCheckedProperty.DefaultValue is bool value ? value : default;
        private static readonly Color TextColorDefaultValue = MC.RadioButton.TextColorProperty.DefaultValue is Color value ? value : default;
        private static readonly TextTransform TextTransformDefaultValue = MC.RadioButton.TextTransformProperty.DefaultValue is TextTransform value ? value : default;

        public RadioButtonHandler(NativeComponentRenderer renderer, MC.RadioButton radioButtonControl) : base(renderer, radioButtonControl)
        {
            RadioButtonControl = radioButtonControl ?? throw new ArgumentNullException(nameof(radioButtonControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.RadioButton RadioButtonControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.RadioButton.BorderColor):
                    RadioButtonControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue, BorderColorDefaultValue);
                    break;
                case nameof(MC.RadioButton.BorderWidth):
                    RadioButtonControl.BorderWidth = AttributeHelper.StringToDouble((string)attributeValue, BorderWidthDefaultValue);
                    break;
                case nameof(MC.RadioButton.CharacterSpacing):
                    RadioButtonControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(MC.RadioButton.CornerRadius):
                    RadioButtonControl.CornerRadius = AttributeHelper.GetInt(attributeValue, CornerRadiusDefaultValue);
                    break;
                case nameof(MC.RadioButton.FontAttributes):
                    RadioButtonControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(MC.RadioButton.FontAutoScalingEnabled):
                    RadioButtonControl.FontAutoScalingEnabled = AttributeHelper.GetBool(attributeValue, FontAutoScalingEnabledDefaultValue);
                    break;
                case nameof(MC.RadioButton.FontFamily):
                    RadioButtonControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(MC.RadioButton.FontSize):
                    RadioButtonControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(MC.RadioButton.GroupName):
                    RadioButtonControl.GroupName = (string)attributeValue ?? GroupNameDefaultValue;
                    break;
                case nameof(MC.RadioButton.IsChecked):
                    RadioButtonControl.IsChecked = AttributeHelper.GetBool(attributeValue, IsCheckedDefaultValue);
                    break;
                case nameof(MC.RadioButton.TextColor):
                    RadioButtonControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(MC.RadioButton.TextTransform):
                    RadioButtonControl.TextTransform = (TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
