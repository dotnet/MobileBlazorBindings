// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;

namespace HybridApp
{
    public class App : Application
    {
        public App(string[] args = null, IFileProvider fileProvider = null)
        {
            var hostBuilder = MobileBlazorBindingsHost.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorHybrid();

                    // Register app-specific services
                    services.AddSingleton<CounterState>();
                })
                .UseWebRoot("wwwroot");

            if (fileProvider != null)
            {
                hostBuilder.UseStaticFiles(fileProvider);
            }
            else
            {
                hostBuilder.UseStaticFiles();
            }
            var host = hostBuilder.Build();

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
