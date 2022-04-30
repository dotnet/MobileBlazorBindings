// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using HybridAuthApp;
using Microsoft.MobileBlazorBindings;
using Microsoft.MobileBlazorBindings.Authentication;

namespace HybridAuthSample
{
    public static class MauiProgram
    {
        private const string BaseUrl = "https://demo.identityserver.io/api/";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMobileBlazorBindings()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddProtectedStorage();
            builder.Services.AddSingleton<CounterState>();

            builder.Services
                .AddOidcAuthentication(options =>
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
            builder.Services.AddHttpClient("demo",
                client => client.BaseAddress = new Uri(BaseUrl))
               .AddHttpMessageHandler(() => new ApiAuthorizationMessageHandler(BaseUrl));

            // Add the http client as the default to inject.
            builder.Services.AddScoped<HttpClient>(sp =>
            {
                var accessTokenProvider = sp.GetRequiredService<IAccessTokenProvider>();
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                ApiAuthorizationMessageHandler.RegisterTokenProvider(BaseUrl, accessTokenProvider);
                return httpClientFactory.CreateClient("demo");
            });

            return builder.Build();
        }
    }
}