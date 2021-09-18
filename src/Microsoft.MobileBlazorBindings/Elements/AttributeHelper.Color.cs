// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Graphics;
using System;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        private const string DefaultColorString = "default";

        private static readonly Color DefaultColor = new();

        /// <summary>
        /// Helper method to serialize <see cref="Color" /> objects.
        /// </summary>
        public static string ColorToString(Color color)
        {
            if (color is null)
            {
                return null;
            }

            if (color.Equals(DefaultColor))
            {
                return DefaultColorString;
            }

            return color.ToRgbaHex(true);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Color" /> objects.
        /// </summary>
        public static Color StringToColor(object colorString, Color defaultValueIfNull = default)
        {
            if (colorString is null)
            {
                return defaultValueIfNull;
            }
            if (!(colorString is string colorAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(colorString));
            }
            //if (colorAsString?.Length != 8 && colorAsString?.Length != DefaultColorString.Length)
            //{
            //    throw new ArgumentException($"Invalid color string '{colorString}'. Expected a hex color in the form 'AARRGGBB' or '{DefaultColorString}'.", nameof(colorString));
            //}
            if (colorAsString == DefaultColorString)
            {
                return DefaultColor;
            }
            return Color.FromRgba(colorAsString);
        }
    }
}
