// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;

namespace MobileBlazorBindingsWeather
{
    public class App : Application
    {
        public App(string[] args = null)
        {
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    services.AddSingleton<WeatherService>();
                })
                .Build();

            MainPage = new ContentPage();
            host.AddComponent<WeatherApp>(parent: MainPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
