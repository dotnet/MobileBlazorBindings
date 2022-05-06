// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace MobileBlazorBindingsWeather
{
    public class WeatherService
    {
        private int _temperature = 70;

        public WeatherReport GetWeatherReport()
        {
            var rand = (new Random(_temperature).NextDouble() * 2d) - 1d; // rand = [-1.0, 1.0)

            // TODO: Get this from an actual weather service!
            return new WeatherReport(_temperature,
                new[]
                {
                    new WeatherDataItem("Pressure", 10 + (int)(rand * 5d), "in"),
                    new WeatherDataItem("UV Index", 5 + (int)(rand * 5d), "of 10"),
                    new WeatherDataItem("Wind Speed", 5 + (int)(rand * 5d), "mph"),
                    new WeatherDataItem("Humidity", 65 + (int)(rand * 10d), "%"),
                    new WeatherDataItem("Min Temp", 40 + (int)(rand * 20d), "°F"),
                    new WeatherDataItem("Max Temp", 80 + (int)(rand * 20d), "°F"),
                });
        }

        public void CreateNewWeather(int temperature)
        {
            _temperature = temperature;
        }
    }
}
