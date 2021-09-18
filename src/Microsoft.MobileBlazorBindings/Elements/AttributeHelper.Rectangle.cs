// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="Thickness" /> objects.
        /// </summary>
        public static string RectangleToString(Rectangle rectangle)
        {
            return rectangle.ToString();
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Thickness" /> objects.
        /// </summary>
        public static Rectangle StringToRectangle(object rectangleString, Rectangle defaultValueIfNull = default)
        {
            if (rectangleString is null)
            {
                return defaultValueIfNull;
            }
            if (!(rectangleString is string rectangleAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(rectangleString));
            }

            return Rectangle.TryParse(rectangleAsString, out var rectangle)
                ? rectangle
                : throw new ArgumentException("Cannot parse Rectangle string.");
        }
    }
}
