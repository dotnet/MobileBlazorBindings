namespace BlazorNativeWeather
{
    public class WeatherService
    {
        public static WeatherReport GetWeatherReport()
        {
            // TODO: Get this from an actual weather service!
            return new WeatherReport(76,
                new[]
                {
                    new WeatherDataItem("Pressure", 10, "in"),
                    new WeatherDataItem("UV Index", 3, "of 10"),
                    new WeatherDataItem("Wind Speed", 0, "mph"),
                    new WeatherDataItem("Humidity", 65, "%"),
                    new WeatherDataItem("Min Temp", 50, "°F"),
                    new WeatherDataItem("Max Temp", 80, "°F"),
                });
        }
    }
}
