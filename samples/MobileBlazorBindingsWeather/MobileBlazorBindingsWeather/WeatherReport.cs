using System.Collections.Generic;

namespace MobileBlazorBindingsWeather
{
    public class WeatherReport
    {
        public WeatherReport(int temperature, IList<WeatherDataItem> dataItems)
        {
            Temperature = temperature;
            DataItems = dataItems ?? throw new System.ArgumentNullException(nameof(dataItems));
        }

        public int Temperature { get; }
        public IList<WeatherDataItem> DataItems { get; }
    }
}
