// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Globalization;

namespace BlinForms.Framework.Controls
{
    public static partial class AttributeHelper
    {
        public static bool GetBool(object value)
        {
            return (value == null)
                ? false
                : (bool)value;
        }

        public static int GetInt(object value)
        {
            return (value == null)
                ? 0
                : int.Parse((string)value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Helper method to serialize <see cref="double" /> objects.
        /// </summary>
        public static string DoubleToString(double color)
        {
            return color.ToString("R", CultureInfo.InvariantCulture); // "R" --> Round-trip format specifier guarantees fidelity when parsing
        }

        /// <summary>
        /// Helper method to deserialize <see cref="double" /> objects.
        /// </summary>
        public static double StringToDouble(string doubleString)
        {
            return double.Parse(doubleString, CultureInfo.InvariantCulture);
        }
    }
}
