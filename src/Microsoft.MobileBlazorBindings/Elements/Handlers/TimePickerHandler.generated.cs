// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.MobileBlazorBindings.Core;
using System;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TimePickerHandler : ViewHandler
    {
        private static readonly double CharacterSpacingDefaultValue = MC.TimePicker.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly MC.FontAttributes FontAttributesDefaultValue = MC.TimePicker.FontAttributesProperty.DefaultValue is MC.FontAttributes value ? value : default;
        private static readonly bool FontAutoScalingEnabledDefaultValue = MC.TimePicker.FontAutoScalingEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string FontFamilyDefaultValue = MC.TimePicker.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = MC.TimePicker.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly string FormatDefaultValue = MC.TimePicker.FormatProperty.DefaultValue is string value ? value : default;
        private static readonly Color TextColorDefaultValue = MC.TimePicker.TextColorProperty.DefaultValue is Color value ? value : default;
        private static readonly TimeSpan TimeDefaultValue = MC.TimePicker.TimeProperty.DefaultValue is TimeSpan value ? value : default;

        public TimePickerHandler(NativeComponentRenderer renderer, MC.TimePicker timePickerControl) : base(renderer, timePickerControl)
        {
            TimePickerControl = timePickerControl ?? throw new ArgumentNullException(nameof(timePickerControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.TimePicker TimePickerControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.TimePicker.CharacterSpacing):
                    TimePickerControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(MC.TimePicker.FontAttributes):
                    TimePickerControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(MC.TimePicker.FontAutoScalingEnabled):
                    TimePickerControl.FontAutoScalingEnabled = AttributeHelper.GetBool(attributeValue, FontAutoScalingEnabledDefaultValue);
                    break;
                case nameof(MC.TimePicker.FontFamily):
                    TimePickerControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(MC.TimePicker.FontSize):
                    TimePickerControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(MC.TimePicker.Format):
                    TimePickerControl.Format = (string)attributeValue ?? FormatDefaultValue;
                    break;
                case nameof(MC.TimePicker.TextColor):
                    TimePickerControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(MC.TimePicker.Time):
                    TimePickerControl.Time = AttributeHelper.StringToTimeSpan(attributeValue, TimeDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
