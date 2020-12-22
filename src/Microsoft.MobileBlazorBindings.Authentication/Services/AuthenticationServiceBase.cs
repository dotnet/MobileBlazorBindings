// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Base class for authentication services.
    /// </summary>
    /// <typeparam name="TAccount">The account type.</typeparam>
    /// <typeparam name="TProviderOptions">The provider options.</typeparam>
    public abstract class AuthenticationServiceBase<TAccount, TProviderOptions>
        : AuthenticationStateProvider, IDisposable
        where TAccount : RemoteUserAccount
        where TProviderOptions : new()
    {
        protected static readonly string[] ProtocolClaims = new string[] { "nonce", "at_hash", "iat", "nbf", "exp", "aud", "iss", "c_hash" };

        // We cache the user claims for 60 seconds to avoid spamming the server.
        // But we also want additional claims that are added to appear in the app fairly quickly.
        private static readonly TimeSpan _userCacheRefreshInterval = TimeSpan.FromSeconds(60);
        private static readonly IEqualityComparer<ClaimsPrincipal> ClaimsPrincipalEqualityComparer = new ClaimsPrincipalEqualityComparer();
        private ClaimsPrincipal _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());

        // This defaults to 1/1/1970
        private DateTimeOffset _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);

        /// <summary>
        /// Gets the <see cref="AccountClaimsPrincipalFactory{TAccount}"/> to map accounts to <see cref="ClaimsPrincipal"/>.
        /// </summary>
        protected AccountClaimsPrincipalFactory<TAccount> AccountClaimsPrincipalFactory { get; }

        /// <summary>
        /// Gets the options for the underlying JavaScript library handling the authentication operations.
        /// </summary>
        protected RemoteAuthenticationOptions<TProviderOptions> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationServiceBase{TAccount, TProviderOptions}"/> class.
        /// </summary>
        /// <param name="options">The options for this <see cref="AuthenticationServiceBase{TAccount, TProviderOptions}"/></param>
        /// <param name="accountClaimsPrincipalFactory">The factory to convert the <typeparamref name="TAccount"/> into a <see cref="ClaimsPrincipal"/></param>
        public AuthenticationServiceBase(RemoteAuthenticationOptions<TProviderOptions> options, AccountClaimsPrincipalFactory<TAccount> accountClaimsPrincipalFactory)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            AccountClaimsPrincipalFactory = accountClaimsPrincipalFactory ?? throw new ArgumentNullException(nameof(accountClaimsPrincipalFactory));
            Options = options;
        }

        /// <inheritdoc />
        public override async Task<AuthenticationState> GetAuthenticationStateAsync() => new AuthenticationState(await GetUser(useCache: true));

        /// <summary>
        /// Gets the users and converts to into a <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <param name="useCache"></param>
        /// <returns></returns>
        protected async Task<ClaimsPrincipal> GetUser(bool useCache = false)
        {
            var now = DateTimeOffset.Now;
            if (useCache && now < _userLastCheck + _userCacheRefreshInterval)
            {
                return _cachedUser;
            }

            try
            {
                var authenticatedUser = await GetAuthenticatedUser();
                if (!ClaimsPrincipalEqualityComparer.Equals(_cachedUser, authenticatedUser))
                {
                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
                }
                _cachedUser = authenticatedUser;
                _userLastCheck = now;
                return _cachedUser;
            }
            catch
            {
                // only throw when not using the cache (e.g. after a sign in or sign-out operation).
                if (!useCache)
                {
                    throw;
                }
                else
                {
                    // pass through the AccountClaimsPrincipalFactory to facilitate possible restore from storage.
                    // this is equal to silent fail of the authentication in the javascript version where a user
                    // is returned from javascript.
                    var recreatedUser = await AccountClaimsPrincipalFactory.CreateUserAsync(null, Options.UserOptions);
                    if (!ClaimsPrincipalEqualityComparer.Equals(_cachedUser, recreatedUser))
                    {
                        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(recreatedUser)));
                    }
                    _cachedUser = recreatedUser;
                    _userLastCheck = now;
                    return _cachedUser;
                }
            }

        }

        /// <summary>
        /// Adds the claims from the id token to the <typeparamref name="TAccount"/> instance.
        /// </summary>
        /// <param name="account">The account type.</param>
        /// <param name="idToken">The id token.</param>
        protected static void AddIdTokenClaimsToAccount(TAccount account, JwtSecurityToken idToken)
        {
            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (idToken is null)
            {
                throw new ArgumentNullException(nameof(idToken));
            }

            foreach (var claim in idToken.Claims)
            {
                if (!account.AdditionalProperties.ContainsKey(claim.Type) && !ProtocolClaims.Contains(claim.Type))
                {
                    account.AdditionalProperties.Add(claim.Type, claim.Value);
                }
            }
        }

        /// <summary>
        /// Gets the authenticated user from a userinfo endpoint and
        /// converts the claims from the id token plus the user into
        /// a <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <returns>The <see cref="ClaimsPrincipal"/></returns>
        protected abstract Task<ClaimsPrincipal> GetAuthenticatedUser();

        /// <summary>
        /// Method that can be overriden to dispose of managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// A value indicating whether the Dispose method is called
        /// by Dispose or by the finalizer.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~AuthenticationServiceBase()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Disposes the Authentication service.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}