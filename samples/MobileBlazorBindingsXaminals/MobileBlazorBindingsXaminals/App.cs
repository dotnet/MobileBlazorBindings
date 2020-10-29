// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;

namespace MobileBlazorBindingsXaminals
{
    public class App : Application
    {
        public IHost AppHost { get; }

        public App(string[] args = null)
        {
            AppHost = MobileBlazorBindingsHost.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    //services.AddSingleton<AppState>();
                    services.AddSingleton<ShellNavigationManager>();
                })
                .Build();

            MainPage = new ContentPage();
            AppHost.AddComponent<AppShell>(parent: this);
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
