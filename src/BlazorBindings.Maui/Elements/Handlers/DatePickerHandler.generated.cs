// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class DatePickerHandler : ViewHandler
    {
        private static readonly double CharacterSpacingDefaultValue = MC.DatePicker.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly DateTime DateDefaultValue = MC.DatePicker.DateProperty.DefaultValue is DateTime value ? value : default;
        private static readonly MC.FontAttributes FontAttributesDefaultValue = MC.DatePicker.FontAttributesProperty.DefaultValue is MC.FontAttributes value ? value : default;
        private static readonly bool FontAutoScalingEnabledDefaultValue = MC.DatePicker.FontAutoScalingEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string FontFamilyDefaultValue = MC.DatePicker.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = MC.DatePicker.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly string FormatDefaultValue = MC.DatePicker.FormatProperty.DefaultValue is string value ? value : default;
        private static readonly DateTime MaximumDateDefaultValue = MC.DatePicker.MaximumDateProperty.DefaultValue is DateTime value ? value : default;
        private static readonly DateTime MinimumDateDefaultValue = MC.DatePicker.MinimumDateProperty.DefaultValue is DateTime value ? value : default;
        private static readonly Color TextColorDefaultValue = MC.DatePicker.TextColorProperty.DefaultValue is Color value ? value : default;

        public DatePickerHandler(NativeComponentRenderer renderer, MC.DatePicker datePickerControl) : base(renderer, datePickerControl)
        {
            DatePickerControl = datePickerControl ?? throw new ArgumentNullException(nameof(datePickerControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.DatePicker DatePickerControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.DatePicker.CharacterSpacing):
                    DatePickerControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(MC.DatePicker.Date):
                    DatePickerControl.Date = AttributeHelper.StringToDateTime(attributeValue, DateDefaultValue);
                    break;
                case nameof(MC.DatePicker.FontAttributes):
                    DatePickerControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(MC.DatePicker.FontAutoScalingEnabled):
                    DatePickerControl.FontAutoScalingEnabled = AttributeHelper.GetBool(attributeValue, FontAutoScalingEnabledDefaultValue);
                    break;
                case nameof(MC.DatePicker.FontFamily):
                    DatePickerControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(MC.DatePicker.FontSize):
                    DatePickerControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(MC.DatePicker.Format):
                    DatePickerControl.Format = (string)attributeValue ?? FormatDefaultValue;
                    break;
                case nameof(MC.DatePicker.MaximumDate):
                    DatePickerControl.MaximumDate = AttributeHelper.StringToDateTime(attributeValue, MaximumDateDefaultValue);
                    break;
                case nameof(MC.DatePicker.MinimumDate):
                    DatePickerControl.MinimumDate = AttributeHelper.StringToDateTime(attributeValue, MinimumDateDefaultValue);
                    break;
                case nameof(MC.DatePicker.TextColor):
                    DatePickerControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
