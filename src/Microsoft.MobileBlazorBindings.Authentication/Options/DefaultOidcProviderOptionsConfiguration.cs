// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Options;
using System;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    internal class DefaultOidcOptionsConfiguration : IPostConfigureOptions<RemoteAuthenticationOptions<OidcProviderOptions>>
    {
        private readonly string _baseUri;

        public DefaultOidcOptionsConfiguration(string baseUri) => _baseUri = baseUri;

        public void Configure(RemoteAuthenticationOptions<OidcProviderOptions> options)
        {
            options.UserOptions.AuthenticationType ??= options.ProviderOptions.ClientId;

            var redirectUri = options.ProviderOptions.RedirectUri;
            if (redirectUri == null || !Uri.TryCreate(redirectUri, UriKind.Absolute, out _))
            {
                redirectUri ??= "authentication/login-callback";
                options.ProviderOptions.RedirectUri = new Uri(new Uri(_baseUri), redirectUri).AbsoluteUri;
            }

            var logoutUri = options.ProviderOptions.PostLogoutRedirectUri;
            if (logoutUri == null || !Uri.TryCreate(logoutUri, UriKind.Absolute, out _))
            {
                logoutUri ??= "authentication/logout-callback";
                options.ProviderOptions.PostLogoutRedirectUri = new Uri(new Uri(_baseUri), logoutUri).AbsoluteUri;
            }
        }

        public void PostConfigure(string name, RemoteAuthenticationOptions<OidcProviderOptions> options)
        {
            if (string.Equals(name, Options.DefaultName, StringComparison.Ordinal))
            {
                Configure(options);
            }
        }
    }
}
