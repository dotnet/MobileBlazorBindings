// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.Options;
using System;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    internal class DefaultApiAuthorizationOptionsConfiguration : IPostConfigureOptions<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>>
    {
        private readonly string _applicationName;
        private readonly string _baseUri;

        public DefaultApiAuthorizationOptionsConfiguration(string baseUri, string applicationName)
        {
            _applicationName = applicationName;
            _baseUri = baseUri;
        }

        public void Configure(RemoteAuthenticationOptions<ApiAuthorizationProviderOptions> options)
        {
            if (!string.IsNullOrEmpty(_baseUri))
            {
                options.ProviderOptions.ConfigurationEndpoint ??= new Uri(new Uri(_baseUri), $"_configuration/{_applicationName}").ToString();
                options.AuthenticationPaths.RemoteRegisterPath ??= new Uri(new Uri(_baseUri), "Identity/Account/Register").ToString();
                options.AuthenticationPaths.RemoteProfilePath ??= new Uri(new Uri(_baseUri), "Identity/Account/Manage").ToString();
            }
            options.UserOptions.ScopeClaim ??= "scope";
            options.UserOptions.RoleClaim ??= "role";
            options.UserOptions.AuthenticationType ??= _applicationName;
        }

        public void PostConfigure(string name, RemoteAuthenticationOptions<ApiAuthorizationProviderOptions> options)
        {
            if (string.Equals(name, Options.DefaultName, System.StringComparison.Ordinal))
            {
                Configure(options);
            }
        }
    }
}
