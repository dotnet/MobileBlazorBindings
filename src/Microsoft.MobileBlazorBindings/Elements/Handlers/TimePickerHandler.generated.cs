// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TimePickerHandler : ViewHandler
    {
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
                    TimePickerControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.TimePicker.FontAttributes):
                    TimePickerControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.TimePicker.FontFamily):
                    TimePickerControl.FontFamily = (string)attributeValue;
                    break;
                case nameof(XF.TimePicker.FontSize):
                    TimePickerControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, -1.00);
                    break;
                case nameof(XF.TimePicker.Format):
                    TimePickerControl.Format = (string)attributeValue ?? "t";
                    break;
                case nameof(XF.TimePicker.TextColor):
                    TimePickerControl.TextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.TimePicker.TextTransform):
                    TimePickerControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)XF.TextTransform.Default);
                    break;
                case nameof(XF.TimePicker.Time):
                    TimePickerControl.Time = AttributeHelper.StringToTimeSpan(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
