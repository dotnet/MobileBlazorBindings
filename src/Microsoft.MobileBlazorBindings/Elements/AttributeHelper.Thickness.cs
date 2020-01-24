// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Globalization;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="Thickness" /> objects.
        /// </summary>
        public static string ThicknessToString(Thickness thickness)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0},{1},{2},{3}",
                thickness.Left,
                thickness.Top,
                thickness.Right,
                thickness.Bottom);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Thickness" /> objects.
        /// </summary>
        public static Thickness StringToThickness(object thicknessString, Thickness defaultValueIfNull = default)
        {
            if (thicknessString is null)
            {
                return defaultValueIfNull;
            }
            if (!(thicknessString is string thicknessAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(thicknessString));
            }

            var marginStringParts = thicknessAsString.Split(',');

            return new Thickness(
                left: StringToDouble(marginStringParts[0]),
                top: StringToDouble(marginStringParts[1]),
                right: StringToDouble(marginStringParts[2]),
                bottom: StringToDouble(marginStringParts[3]));
        }
    }
}
