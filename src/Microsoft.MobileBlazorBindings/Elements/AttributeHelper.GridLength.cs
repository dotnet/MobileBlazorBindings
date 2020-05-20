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
        /// Helper method to serialize <see cref="GridLength" /> objects.
        /// </summary>
        public static string GridLengthToString(GridLength gridLength)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0:R},{1}", // "R" --> Round-trip format specifier guarantees fidelity when parsing System.Double
                gridLength.Value,
                (int)gridLength.GridUnitType);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="GridLength" /> objects.
        /// </summary>
        public static GridLength StringToGridLength(object gridLengthString, GridLength defaultValueIfNull = default)
        {
            if (gridLengthString is null)
            {
                return defaultValueIfNull;
            }
            if (!(gridLengthString is string gridLengthAsString))
            {
                throw new ArgumentNullException(nameof(gridLengthString));
            }

            var stringParts = gridLengthAsString.Split(',');

            if (stringParts.Length != 2)
            {
                throw new ArgumentNullException(nameof(gridLengthString), message: "Expected value to have one comma (',') in it.");
            }

            return
                new GridLength(
                    double.Parse(stringParts[0], CultureInfo.InvariantCulture),
                    (GridUnitType)int.Parse(stringParts[1], CultureInfo.InvariantCulture));
        }
    }
}
