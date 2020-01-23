﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Globalization;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public static partial class AttributeHelper
    {
        public static bool GetBool(object value, bool defaultValueIfNull = default)
        {
            return (value == null)
                ? defaultValueIfNull
                : (bool)value;
        }

        public static int GetInt(object value, int defaultValueIfNull = default)
        {
            return (value == null)
                ? defaultValueIfNull
                : int.Parse((string)value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Helper method to serialize <see cref="System.Double" /> objects.
        /// </summary>
        public static string DoubleToString(double color)
        {
            return color.ToString("R", CultureInfo.InvariantCulture); // "R" --> Round-trip format specifier guarantees fidelity when parsing
        }

        /// <summary>
        /// Helper method to deserialize <see cref="System.Double" /> objects.
        /// </summary>
        public static double StringToDouble(string doubleString, double defaultValueIfNull = default)
        {
            if (doubleString is null)
            {
                return defaultValueIfNull;
            }
            return double.Parse(doubleString, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Helper method to serialize <see cref="System.Single" /> objects.
        /// </summary>
        public static string SingleToString(float color)
        {
            return color.ToString("R", CultureInfo.InvariantCulture); // "R" --> Round-trip format specifier guarantees fidelity when parsing
        }

        /// <summary>
        /// Helper method to deserialize <see cref="System.Single" /> objects.
        /// </summary>
        public static float StringToSingle(string singleString, float defaultValueIfNull = default)
        {
            if (singleString is null)
            {
                return defaultValueIfNull;
            }
            return float.Parse(singleString, CultureInfo.InvariantCulture);
        }
    }
}
