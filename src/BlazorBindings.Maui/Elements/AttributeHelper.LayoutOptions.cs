// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="LayoutOptions" /> objects.
        /// </summary>
        public static string LayoutOptionsToString(LayoutOptions layoutOptions)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0},{1}",
                (int)layoutOptions.Alignment,
                layoutOptions.Expands);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="LayoutOptions" /> objects.
        /// </summary>
        public static LayoutOptions StringToLayoutOptions(object layoutString, LayoutOptions defaultValueIfNull = default)
        {
            if (layoutString is null)
            {
                return defaultValueIfNull;
            }
            if (!(layoutString is string layoutAsString))
            {
                throw new ArgumentNullException(nameof(layoutString));
            }

            var stringParts = layoutAsString.Split(',');

            if (stringParts.Length != 2)
            {
                throw new ArgumentNullException(nameof(layoutString), message: "Expected value to have one comma (',') in it.");
            }

            return
                new LayoutOptions(
                    (LayoutAlignment)int.Parse(stringParts[0], CultureInfo.InvariantCulture),
                    bool.Parse(stringParts[1]));
        }
    }
}
