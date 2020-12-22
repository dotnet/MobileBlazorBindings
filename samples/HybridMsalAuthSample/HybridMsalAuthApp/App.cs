// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Http;
using Microsoft.Identity.Client;
using Microsoft.MobileBlazorBindings;
using Microsoft.MobileBlazorBindings.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace HybridMsalAuthApp
{
    public class App : Application
    {
        private const string BaseUrl = "https://demo.identityserver.io/api/";

        // clientID is also in Android project in MainActivity.
        private const string ClientId = "{clientId}";
        private const string TenantId = "{TenantId}";

        public App(string[] args = null, IFileProvider fileProvider = null)
        {
            var hostBuilder = MobileBlazorBindingsHost.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorHybrid();

                    // Add protected storage for storage of refresh tokens
                    services.AddProtectedStorage();

                    // Register app-specific services
                    services.AddSingleton<CounterState>();

                    services.AddMsalAuthentication(sp =>
                    {
                        var builder = PublicClientApplicationBuilder
                            .Create(ClientId)
                            .WithTenantId(TenantId);

                        if (Device.RuntimePlatform == Device.WPF)
                        {
                            builder = builder.WithRedirectUri("http://localhost");
                        } 
                        else if (Device.RuntimePlatform == Device.iOS)
                        {
                            // should be the same as Entitlements.plist
                            builder.WithIosKeychainSecurityGroup("com.companyname");
                        }

                        return builder.Build();
                    });
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
