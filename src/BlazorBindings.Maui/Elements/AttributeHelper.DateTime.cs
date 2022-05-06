// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Globalization;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="DateTime" /> objects.
        /// </summary>
        public static string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("o", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="DateTime" /> objects.
        /// </summary>
        public static DateTime StringToDateTime(object dateTimeString, DateTime defaultValueIfNull = default)
        {
            if (dateTimeString is null)
            {
                return defaultValueIfNull;
            }
            if (!(dateTimeString is string dateTimeAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(dateTimeString));
            }

            return DateTime.Parse(dateTimeAsString, null, DateTimeStyles.RoundtripKind);
        }
    }
}
