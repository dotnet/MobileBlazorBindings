// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui;
using System;
using System.Globalization;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="CornerRadius" /> objects.
        /// </summary>
        public static string CornerRadiusToString(CornerRadius cornerRadius)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0},{1},{2},{3}",
                cornerRadius.TopLeft,
                cornerRadius.TopRight,
                cornerRadius.BottomLeft,
                cornerRadius.BottomRight);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="CornerRadius" /> objects.
        /// </summary>
        public static CornerRadius StringToCornerRadius(object cornerRadiusString, CornerRadius defaultValueIfNull = default)
        {
            if (cornerRadiusString is null)
            {
                return defaultValueIfNull;
            }
            if (!(cornerRadiusString is string cornerRadiusAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(cornerRadiusString));
            }

            var marginStringParts = cornerRadiusAsString.Split(',');

            return new CornerRadius(
                topLeft: StringToDouble(marginStringParts[0]),
                topRight: StringToDouble(marginStringParts[1]),
                bottomLeft: StringToDouble(marginStringParts[2]),
                bottomRight: StringToDouble(marginStringParts[3]));
        }
    }
}
