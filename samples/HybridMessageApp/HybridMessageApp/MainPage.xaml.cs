// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using HybridMessageApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HybridMessageApp
{
    public partial class MainPage : Application
    {
        public static IHost Host { get; private set; }

        public MainPage(string[] args = null, IFileProvider fileProvider = null)
        {
            var hostBuilder = MobileBlazorBindingsHost.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorHybrid();

                    // Register app-specific services
                    services.AddSingleton<AppState>();
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
            Host = hostBuilder.Build();

            InitializeComponent();

            FolderWebView.Host = Host;

            Flyout.IsPresented = false;
            WorkaroundDisplayIssue();
        }

        private async void WorkaroundDisplayIssue()
        {
            await Task.Delay(1000);
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                Flyout.IsPresented = false;
            });
            await Task.Delay(1);
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                Flyout.IsPresented = true;
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
