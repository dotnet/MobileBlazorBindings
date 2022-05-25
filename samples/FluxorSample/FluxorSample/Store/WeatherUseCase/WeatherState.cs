using Fluxor;
using FluxorSample.Data;

namespace FluxorSample.Store.WeatherUseCase
{
    [FeatureState]
    public class WeatherState
    {
        public bool IsLoading { get; }
        public IEnumerable<WeatherForecast> Forecasts { get; }

        private WeatherState()
        {
            Forecasts = Array.Empty<WeatherForecast>();
        }

        public WeatherState(bool isLoading, IEnumerable<WeatherForecast> forecasts)
        {
            IsLoading = isLoading;
            Forecasts = forecasts ?? Array.Empty<WeatherForecast>();
        }
    }
}
