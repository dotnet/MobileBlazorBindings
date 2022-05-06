// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Graphics;
using System;
using System.Globalization;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="Point" /> objects.
        /// </summary>
        public static string PointToString(Point point)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0},{1}",
                point.X,
                point.Y);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Point" /> objects.
        /// </summary>
        public static Point StringToPoint(object pointString, Point defaultValueIfNull = default)
        {
            if (pointString is null)
            {
                return defaultValueIfNull;
            }
            if (!(pointString is string pointAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(pointString));
            }

            var marginStringParts = pointAsString.Split(',');

            return new Point(
                x: StringToDouble(marginStringParts[0]),
                y: StringToDouble(marginStringParts[1]));
        }
    }
}
