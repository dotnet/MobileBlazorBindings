// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using HybridApp.Data;
using Microsoft.MobileBlazorBindings;

namespace HybridApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMobileBlazorBindings()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<CounterState>();

            return builder.Build();
        }
    }
}