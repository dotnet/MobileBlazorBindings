using HybridApp.Data;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.MobileBlazorBindings;

namespace HybridApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .RegisterBlazorMauiWebView()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddBlazorWebView();
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddSingleton<CounterState>();

            builder.Services
                .AddSingleton<ShellNavigationManager>()
                .AddScoped<MobileBlazorBindingsRenderer>();

            return builder.Build();
        }
    }
}