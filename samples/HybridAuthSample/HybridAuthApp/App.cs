// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Http;
using Microsoft.MobileBlazorBindings;
using Microsoft.MobileBlazorBindings.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace HybridAuthApp
{
    public class App : Application
    {
        private const string BaseUrl = "https://demo.identityserver.io/api/";

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
                    services.AddOidcAuthentication(options =>
                    {
                        options.ProviderOptions.Authority = "https://demo.identityserver.io/";
                        options.ProviderOptions.ClientId = "interactive.public";
                        options.ProviderOptions.DefaultScopes.Add("openid");
                        options.ProviderOptions.DefaultScopes.Add("profile");
                        options.ProviderOptions.DefaultScopes.Add("api");

                        // request offline_access. This scope will enable refresh tokens to
                        // be sent to this application. Refresh tokens make it possible to
                        // start the application and not having to login again (or start the
                        // browser popup to read cookies).
                        options.ProviderOptions.DefaultScopes.Add("offline_access");
                        options.ProviderOptions.ResponseType = "code";
                    });

                    // Configure HttpClient for use when talking to server backend. We use a hardcoded URL for now.
                    services.AddHttpClient("demo",
                        client => client.BaseAddress = new Uri(BaseUrl))
                       .AddHttpMessageHandler(() => new ApiAuthorizationMessageHandler(BaseUrl));

                    // Add the http client as the default to inject.
                    services.AddScoped<HttpClient>(sp =>
                    {
                        var accessTokenProvider = sp.GetRequiredService<IAccessTokenProvider>();
                        var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                        ApiAuthorizationMessageHandler.RegisterTokenProvider(BaseUrl, accessTokenProvider);
                        return httpClientFactory.CreateClient("demo");
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
