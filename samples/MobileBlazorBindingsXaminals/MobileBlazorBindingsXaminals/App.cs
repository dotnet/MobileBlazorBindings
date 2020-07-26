﻿// Copyright (c) Microsoft Corporation.
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

        public App()
        {
            AppHost = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    //services.AddSingleton<AppState>();
                    services.AddSingleton<ShellNavigationManager>();
                })
                .Build();

            //We have to set a placeholder MainPage here, because setting it in AddComponent happens after it returns.
            //This causes iOS to kill the app because iOS thinks we never set a page.
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
