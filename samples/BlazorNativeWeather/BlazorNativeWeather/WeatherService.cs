using System;

namespace BlazorNativeWeather
{
    public class WeatherService
    {
        private int _randomSeed;

        public WeatherReport GetWeatherReport()
        {
            var rand = (new Random(_randomSeed).NextDouble() * 2d) - 1d; // rand = [-1.0, 1.0)

            // TODO: Get this from an actual weather service!
            return new WeatherReport(60 + (int)(rand * 40d),
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

        public void RandomizeWeather()
        {
            _randomSeed = new Random(_randomSeed).Next();
        }
    }
}
