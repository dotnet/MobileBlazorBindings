// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;

namespace Microsoft.MobileBlazorBindings.Authentication.Msal
{
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    internal class MsalDefaultOptionsConfiguration : IPostConfigureOptions<RemoteAuthenticationOptions<PublicClientApplicationOptions>>
#pragma warning restore CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    {
        public static void Configure(RemoteAuthenticationOptions<PublicClientApplicationOptions> options)
        {
            options.UserOptions.ScopeClaim ??= "scp";
            options.UserOptions.AuthenticationType ??= options.ProviderOptions.ClientId;
        }

        public void PostConfigure(string name, RemoteAuthenticationOptions<PublicClientApplicationOptions> options)
        {
            if (string.Equals(name, Options.DefaultName, StringComparison.Ordinal))
            {
                Configure(options);
            }
        }
    }
}
