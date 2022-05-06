// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Graphics;
using System;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="Color" /> objects.
        /// </summary>
        public static string ColorToString(Color color)
        {
            if (color is null)
            {
                return null;
            }

            return color.ToRgbaHex(true);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Color" /> objects.
        /// </summary>
        public static Color StringToColor(object colorString, Color defaultValueIfNull = null)
        {
            if (colorString is null)
            {
                return defaultValueIfNull;
            }
            if (!(colorString is string colorAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(colorString));
            }

            return Color.FromRgba(colorAsString);
        }
    }
}
