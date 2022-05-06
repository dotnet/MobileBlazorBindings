// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using System;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="Thickness" /> objects.
        /// </summary>
        public static string RectToString(Rect rectangle)
        {
            return rectangle.ToString();
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Thickness" /> objects.
        /// </summary>
        public static Rect StringToRect(object rectangleString, Rect defaultValueIfNull = default)
        {
            if (rectangleString is null)
            {
                return defaultValueIfNull;
            }
            if (!(rectangleString is string rectangleAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(rectangleString));
            }

            return Rect.TryParse(rectangleAsString, out var rectangle)
                ? rectangle
                : throw new ArgumentException("Cannot parse Rectangle string.");
        }
    }
}
