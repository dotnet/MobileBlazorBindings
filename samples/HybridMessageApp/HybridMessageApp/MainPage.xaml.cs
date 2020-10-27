// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using HybridMessageApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings;
using Microsoft.MobileBlazorBindings.WebView;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HybridMessageApp
{
    public partial class MainPage : Application
    {
        public static IHost Host { get; private set; }

        public MainPage()
        {
            Host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorHybrid();

                    // Register app-specific services
                    services.AddSingleton<AppState>();
                })
                .UseWebRoot("WebUI/wwwroot")
                .UseResourceAssembly(GetType().Assembly)
                .Build();

            InitializeComponent();

            FolderWebView.Host = Host;

            MasterDetails.IsPresented = false;
            WorkaroundDisplayIssue();
        }

        private async void WorkaroundDisplayIssue()
        {
            await Task.Delay(1000);
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                MasterDetails.IsPresented = false;
            });
            await Task.Delay(1);
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                MasterDetails.IsPresented = true;
            });
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
