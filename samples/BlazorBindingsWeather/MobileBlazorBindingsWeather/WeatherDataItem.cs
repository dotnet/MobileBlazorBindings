// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace MobileBlazorBindingsWeather
{
    public class WeatherDataItem
    {
        public WeatherDataItem(string name, int value, string unit = default)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value;
            Unit = unit;
        }

        public string Name { get; }
        public int Value { get; }
        public string Unit { get; }
    }
}
