// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;

namespace MobileBlazorBindingsTodo
{
    public class App : Application
    {
        public IHost AppHost { get; }

        public App(string[] args = null, IServiceCollection additionalServices = null)
        {
            AppHost = MobileBlazorBindingsHost.CreateDefaultBuilder(args)
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
                .Build();

            MainPage = new TabbedPage();
            AppHost.AddComponent<TodoApp>(parent: MainPage);
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
