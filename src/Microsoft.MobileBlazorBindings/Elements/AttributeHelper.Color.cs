// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="Color" /> objects.
        /// </summary>
        public static string ColorToString(Color color)
        {
            var red = (uint)(color.R * 255);
            var green = (uint)(color.G * 255);
            var blue = (uint)(color.B * 255);
            var alpha = (uint)(color.A * 255);
            return $"{alpha:X2}{red:X2}{green:X2}{blue:X2}";
        }

        /// <summary>
        /// Helper method to deserialize <see cref="Color" /> objects.
        /// </summary>
        public static Color StringToColor(string colorString, Color defaultValueIfNull = default)
        {
            if (colorString is null)
            {
                return defaultValueIfNull;
            }
            if (colorString?.Length != 8)
            {
                throw new ArgumentException($"Invalid color string '{colorString}'. Expected a hex color in the form 'AARRGGBB'.", nameof(colorString));
            }
            return FromHex(colorString);
        }

        private static Color FromHex(string hex)
        {
            return Color.FromRgba(
                r: (int)(ToHex(hex[2]) << 4 | ToHex(hex[3])),
                g: (int)(ToHex(hex[4]) << 4 | ToHex(hex[5])),
                b: (int)(ToHex(hex[6]) << 4 | ToHex(hex[7])),
                a: (int)(ToHex(hex[0]) << 4 | ToHex(hex[1])));
        }

        private static uint ToHex(char c)
        {
            ushort x = c;
            if (x >= '0' && x <= '9')
            {
                return (uint)(x - '0');
            }

            x |= 0x20;
            if (x >= 'a' && x <= 'f')
            {
                return (uint)(x - 'a' + 10);
            }
            return 0;
        }
    }
}
