// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class DatePickerHandler : ViewHandler
    {
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
                    DatePickerControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.DatePicker.Date):
                    DatePickerControl.Date = AttributeHelper.StringToDateTime(attributeValue);
                    break;
                case nameof(XF.DatePicker.FontAttributes):
                    DatePickerControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.DatePicker.FontFamily):
                    DatePickerControl.FontFamily = (string)attributeValue;
                    break;
                case nameof(XF.DatePicker.FontSize):
                    DatePickerControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.DatePicker.Format):
                    DatePickerControl.Format = (string)attributeValue ?? "d";
                    break;
                case nameof(XF.DatePicker.MaximumDate):
                    DatePickerControl.MaximumDate = AttributeHelper.StringToDateTime(attributeValue, global::System.DateTime.Parse("2100-12-31T00:00:00.0000000", null, global::System.Globalization.DateTimeStyles.RoundtripKind));
                    break;
                case nameof(XF.DatePicker.MinimumDate):
                    DatePickerControl.MinimumDate = AttributeHelper.StringToDateTime(attributeValue, global::System.DateTime.Parse("1900-01-01T00:00:00.0000000", null, global::System.Globalization.DateTimeStyles.RoundtripKind));
                    break;
                case nameof(XF.DatePicker.TextColor):
                    DatePickerControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.DatePicker.TextTransform):
                    DatePickerControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)XF.TextTransform.Default);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
