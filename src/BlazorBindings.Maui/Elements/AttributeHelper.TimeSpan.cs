// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Globalization;

namespace BlazorBindings.Maui.Elements
{
    public static partial class AttributeHelper
    {
        /// <summary>
        /// Helper method to serialize <see cref="TimeSpan" /> objects.
        /// </summary>
        public static string TimeSpanToString(TimeSpan timeSpan)
        {
            return timeSpan.Ticks.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Helper method to deserialize <see cref="TimeSpan" /> objects.
        /// </summary>
        public static TimeSpan StringToTimeSpan(object timeSpanString, TimeSpan defaultValueIfNull = default)
        {
            if (timeSpanString is null)
            {
                return defaultValueIfNull;
            }
            if (!(timeSpanString is string timeSpanAsString))
            {
                throw new ArgumentException("Expected parameter instance to be a string.", nameof(timeSpanString));
            }

            return new TimeSpan(long.Parse(timeSpanAsString, CultureInfo.InvariantCulture));
        }
    }
}
