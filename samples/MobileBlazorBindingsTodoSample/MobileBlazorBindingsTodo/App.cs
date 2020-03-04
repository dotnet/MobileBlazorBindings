﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventSource;
using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;

namespace MobileBlazorBindingsTodo
{
    public class App : Application
    {
        public IHost AppHost { get; }

        public App(IServiceCollection additionalServices)
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register backend-specific services (e.g. iOS, Android)
                    if (additionalServices != null)
                    {
                        services.AddAdditionalServices(additionalServices);
                    }
                    
                    // Register app-specific services
                    services.AddSingleton<AppState>();
                })

                .ConfigureLogging(d =>
                {
                    d.ClearProviders();
                    d.AddConsole();
                })
                .Build();

            AppHost.AddComponent<TodoApp>(parent: this);
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
