// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        public static object ObjectToAttribute(object value)
        {
            if (value == null || value is string || value is int || value is Delegate)
                return value;

            if (value is bool boolValue)
                return boolValue ? "1" : "0";

            return value switch
            {
                double d => DoubleToString(d),
                float f => SingleToString(f),
                uint ui => UInt32ToString(ui),
                Color color => ColorToString(color),
                Rect rect => RectToString(rect),
                CornerRadius cornerRadius => CornerRadiusToString(cornerRadius),
                DateTime dateTime => DateTimeToString(dateTime),
                GridLength gridLength => GridLengthToString(gridLength),
                LayoutOptions layoutOptions => LayoutOptionsToString(layoutOptions),
                Thickness thickness => ThicknessToString(thickness),
                FlexBasis flexBasis => FlexBasisToString(flexBasis),
                TimeSpan timeSpan => TimeSpanToString(timeSpan),
                Enum => (int)value,
                _ => ObjectToDelegate(value)
            };
        }

        /// <summary>
        /// Helper method to serialize objects.
        /// </summary>
        public static AttributeValueHolder ObjectToDelegate(object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return AttributeValueHolderFactory.FromObject(value);
        }

        /// <summary>
        /// Helper method to deserialize objects.
        /// </summary>
        public static T DelegateToObject<T>(object value, T defaultValueIfNull = default)
        {
            return AttributeValueHolderFactory.ToValue<T>(value, defaultValueIfNull);
        }
    }
}
