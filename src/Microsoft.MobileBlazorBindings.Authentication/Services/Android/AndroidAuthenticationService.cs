// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Options;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// An implementation for <see cref="IAuthenticationService"/> that uses Xamarin.Essentials WebAuthenticator to authenticate the user.
    /// </summary>
    /// <typeparam name="TRemoteAuthenticationState">The state to preserve across authentication operations.</typeparam>
    /// <typeparam name="TAccount">The type of the <see cref="RemoteUserAccount" />.</typeparam>
    /// <typeparam name="TProviderOptions">The options to be passed down to the underlying Authentication library handling the authentication operations.</typeparam>
    public class AndroidAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions> :
        OidcAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions>
        where TRemoteAuthenticationState : OidcAuthenticationState, new()
        where TProviderOptions : new()
        where TAccount : RemoteUserAccount
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options">The options to be passed down to the underlying Authentication library handling the authentication operations.</param>
        /// <param name="tokenCache">The token cache to use to store tokens.</param>
        /// <param name="protectedStorage">The protect storage where refresh tokens will be stored.</param>
        /// <param name="accountClaimsPrincipalFactory">The <see cref="AccountClaimsPrincipalFactory{TAccount}"/> used to generate the <see cref="ClaimsPrincipal"/> for the user.</param>
        public AndroidAuthenticationService(
            IOptionsSnapshot<RemoteAuthenticationOptions<TProviderOptions>> options,
            ITokenCache tokenCache,
            IProtectedStorage protectedStorage,
            AccountClaimsPrincipalFactory<TAccount> accountClaimsPrincipalFactory) : base(options, tokenCache, protectedStorage, accountClaimsPrincipalFactory)
        {
        }

        protected override async Task<string> StartSecureNavigation(Uri startUrl, Uri redirectUrl)
        {
            var authenticationResult = await WebAuthenticator.AuthenticateAsync(startUrl, redirectUrl);

            if (!authenticationResult.Properties.Any())
            {
                return string.Empty;
            }

            return $"?{string.Join("&", authenticationResult.Properties.Select(x => $"{x.Key}={x.Value}"))}";
        }
    }
}
