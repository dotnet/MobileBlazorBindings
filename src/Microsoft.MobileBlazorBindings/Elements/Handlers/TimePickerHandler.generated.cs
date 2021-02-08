// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TimePickerHandler : ViewHandler
    {
        private static readonly double CharacterSpacingDefaultValue = XF.TimePicker.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly XF.FontAttributes FontAttributesDefaultValue = XF.TimePicker.FontAttributesProperty.DefaultValue is XF.FontAttributes value ? value : default;
        private static readonly string FontFamilyDefaultValue = XF.TimePicker.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = XF.TimePicker.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly string FormatDefaultValue = XF.TimePicker.FormatProperty.DefaultValue is string value ? value : default;
        private static readonly XF.Color TextColorDefaultValue = XF.TimePicker.TextColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.TextTransform TextTransformDefaultValue = XF.TimePicker.TextTransformProperty.DefaultValue is XF.TextTransform value ? value : default;
        private static readonly TimeSpan TimeDefaultValue = XF.TimePicker.TimeProperty.DefaultValue is TimeSpan value ? value : default;

        public TimePickerHandler(NativeComponentRenderer renderer, XF.TimePicker timePickerControl) : base(renderer, timePickerControl)
        {
            TimePickerControl = timePickerControl ?? throw new ArgumentNullException(nameof(timePickerControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.TimePicker TimePickerControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.TimePicker.CharacterSpacing):
                    TimePickerControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(XF.TimePicker.FontAttributes):
                    TimePickerControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(XF.TimePicker.FontFamily):
                    TimePickerControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(XF.TimePicker.FontSize):
                    TimePickerControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(XF.TimePicker.Format):
                    TimePickerControl.Format = (string)attributeValue ?? FormatDefaultValue;
                    break;
                case nameof(XF.TimePicker.TextColor):
                    TimePickerControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(XF.TimePicker.TextTransform):
                    TimePickerControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                case nameof(XF.TimePicker.Time):
                    TimePickerControl.Time = AttributeHelper.StringToTimeSpan(attributeValue, TimeDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
