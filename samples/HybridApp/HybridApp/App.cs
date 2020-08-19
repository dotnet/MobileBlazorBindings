// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.MobileBlazorBindings;
using Microsoft.MobileBlazorBindings.WebView;
using Xamarin.Forms;

namespace HybridApp
{
    public class App : Application
    {
        public App()
        {
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorHybrid();

                    // Register app-specific services
                    services.AddSingleton<CounterState>();
                })
                .UseWebRoot("wwwroot")
                .UseStaticFiles()
                .Build();

            MainPage = new ContentPage { Title = "My Application" };
            host.AddComponent<Main>(parent: MainPage);
        }

        public App(IFileProvider fileProvider)
        {
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorHybrid();

                    // Register app-specific services
                    services.AddSingleton<CounterState>();
                })
                .UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = fileProvider,
                })
                .Build();

            MainPage = new ContentPage { Title = "My Application" };
            host.AddComponent<Main>(parent: MainPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
