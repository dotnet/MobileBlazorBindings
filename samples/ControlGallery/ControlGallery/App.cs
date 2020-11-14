// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;

namespace ControlGallery
{
    public class App : Application
    {
        public App(string[] args = null)
        {
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    //services.AddSingleton<AppState>();
                    services.AddSingleton<ShellNavigationManager>();
                })
                .Build();

            MainPage = new ContentPage();
            host.AddComponent<AppShell>(this);
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
