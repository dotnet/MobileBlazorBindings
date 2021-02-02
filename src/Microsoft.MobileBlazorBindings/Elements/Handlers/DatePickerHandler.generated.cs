// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class DatePickerHandler : ViewHandler
    {
        private static readonly double CharacterSpacingDefaultValue = XF.DatePicker.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly DateTime DateDefaultValue = XF.DatePicker.DateProperty.DefaultValue is DateTime value ? value : default;
        private static readonly XF.FontAttributes FontAttributesDefaultValue = XF.DatePicker.FontAttributesProperty.DefaultValue is XF.FontAttributes value ? value : default;
        private static readonly string FontFamilyDefaultValue = XF.DatePicker.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = XF.DatePicker.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly string FormatDefaultValue = XF.DatePicker.FormatProperty.DefaultValue is string value ? value : default;
        private static readonly DateTime MaximumDateDefaultValue = XF.DatePicker.MaximumDateProperty.DefaultValue is DateTime value ? value : default;
        private static readonly DateTime MinimumDateDefaultValue = XF.DatePicker.MinimumDateProperty.DefaultValue is DateTime value ? value : default;
        private static readonly XF.Color TextColorDefaultValue = XF.DatePicker.TextColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.TextTransform TextTransformDefaultValue = XF.DatePicker.TextTransformProperty.DefaultValue is XF.TextTransform value ? value : default;

        public DatePickerHandler(NativeComponentRenderer renderer, XF.DatePicker datePickerControl) : base(renderer, datePickerControl)
        {
            DatePickerControl = datePickerControl ?? throw new ArgumentNullException(nameof(datePickerControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.DatePicker DatePickerControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.DatePicker.CharacterSpacing):
                    DatePickerControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(XF.DatePicker.Date):
                    DatePickerControl.Date = AttributeHelper.StringToDateTime(attributeValue, DateDefaultValue);
                    break;
                case nameof(XF.DatePicker.FontAttributes):
                    DatePickerControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(XF.DatePicker.FontFamily):
                    DatePickerControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(XF.DatePicker.FontSize):
                    DatePickerControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(XF.DatePicker.Format):
                    DatePickerControl.Format = (string)attributeValue ?? FormatDefaultValue;
                    break;
                case nameof(XF.DatePicker.MaximumDate):
                    DatePickerControl.MaximumDate = AttributeHelper.StringToDateTime(attributeValue, MaximumDateDefaultValue);
                    break;
                case nameof(XF.DatePicker.MinimumDate):
                    DatePickerControl.MinimumDate = AttributeHelper.StringToDateTime(attributeValue, MinimumDateDefaultValue);
                    break;
                case nameof(XF.DatePicker.TextColor):
                    DatePickerControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(XF.DatePicker.TextTransform):
                    DatePickerControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
