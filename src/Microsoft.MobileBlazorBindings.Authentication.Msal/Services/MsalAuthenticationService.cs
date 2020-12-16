// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Authentication.Msal
{
    /// <summary>
    /// The default implementation for <see cref="IAuthenticationService"/> that uses Microsoft.Identity.Client to authenticate the user.
    /// </summary>
    /// <typeparam name="TAccount">The type of the <see cref="RemoteUserAccount" />.</typeparam>
    /// <typeparam name="TProviderOptions">The options to be passed down to the underlying Authentication library handling the authentication operations.</typeparam>
    public class MsalAuthenticationService<TAccount, TProviderOptions>
        : AuthenticationServiceBase<TAccount, TProviderOptions>, IAuthenticationService, IAccessTokenProvider
        where TAccount : RemoteUserAccount, new()
        where TProviderOptions : PublicClientApplicationOptions, new()
    {
        private static readonly Uri UserInfoEndpoint = new Uri("https://graph.microsoft.com/v1.0/me");

        private static readonly string[] DefaultScopes = new string[] { "openid", "profile" };

        private readonly PublicClientApplication _clientApplication;

        private string TokenCacheKey => $"{GetType().Name}_token_cache";

        private IEnumerable<string> _currentScopes = DefaultScopes;
        private IAccount _currentAccount;
        private JwtSecurityToken _idToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsalAuthenticationService{TAccount, TProviderOptions}"/> class.
        /// </summary>
        /// <param name="clientApplication">The public client application to use to connect.</param>
        /// <param name="protectedStorage">The protect storage where refresh tokens will be stored.</param>
        /// <param name="accountClaimsPrincipalFactory">The <see cref="AccountClaimsPrincipalFactory{TAccount}"/> used to generate the <see cref="ClaimsPrincipal"/> for the user.</param>
        public MsalAuthenticationService(
            PublicClientApplication clientApplication,
            IProtectedStorage protectedStorage,
            AccountClaimsPrincipalFactory<TAccount> accountClaimsPrincipalFactory) :
            base(new RemoteAuthenticationOptions<TProviderOptions>()
            {
                ProviderOptions = new TProviderOptions()
                {
#pragma warning disable CA1062 // Validate arguments of public methods
                    ClientCapabilities = clientApplication.AppConfig.ClientCapabilities,
                    ClientId = clientApplication.AppConfig.ClientId,
                    ClientName = clientApplication.AppConfig.ClientName,
                    ClientVersion = clientApplication.AppConfig.ClientVersion,
                    EnablePiiLogging = clientApplication.AppConfig.EnablePiiLogging,
                    IsDefaultPlatformLoggingEnabled = clientApplication.AppConfig.IsDefaultPlatformLoggingEnabled,
                    LogLevel = clientApplication.AppConfig.LogLevel,
                    RedirectUri = clientApplication.AppConfig.RedirectUri,
                    TenantId = clientApplication.AppConfig.TenantId,
#pragma warning restore CA1062 // Validate arguments of public methods
                },
            }, accountClaimsPrincipalFactory)
        {
            _clientApplication = clientApplication ?? throw new ArgumentNullException(nameof(clientApplication));

            MsalDefaultOptionsConfiguration.Configure(Options as RemoteAuthenticationOptions<PublicClientApplicationOptions>);

            SetUpSerializationHandlers(protectedStorage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsalAuthenticationService{TAccount, TProviderOptions}"/> class.
        /// </summary>
        /// <param name="options">The options to be passed down to the underlying Authentication library handling the authentication operations.</param>
        /// <param name="protectedStorage">The protect storage where refresh tokens will be stored.</param>
        /// <param name="accountClaimsPrincipalFactory">The <see cref="AccountClaimsPrincipalFactory{TAccount}"/> used to generate the <see cref="ClaimsPrincipal"/> for the user.</param>
        public MsalAuthenticationService(
            IOptionsSnapshot<RemoteAuthenticationOptions<TProviderOptions>> options,
            IProtectedStorage protectedStorage,
            AccountClaimsPrincipalFactory<TAccount> accountClaimsPrincipalFactory) :
            base(options?.Value, accountClaimsPrincipalFactory)
        {
            _clientApplication = (PublicClientApplication)PublicClientApplicationBuilder.CreateWithApplicationOptions(Options.ProviderOptions).Build();

            SetUpSerializationHandlers(protectedStorage);
        }

        private void SetUpSerializationHandlers(IProtectedStorage protectedStorage)
        {
            // these platforms have their own secure storage.
            if (Device.RuntimePlatform == Device.iOS ||
                Device.RuntimePlatform == Device.Android)
            {
                return;
            }

            // set up serialization handlers.
            _clientApplication.UserTokenCache.SetBeforeAccessAsync(async args =>
            {
                if (args.HasStateChanged)
                {
                    return;
                }

                var bytes = await protectedStorage.GetAsync<byte[]>(TokenCacheKey).ConfigureAwait(false);
                if (bytes is null)
                {
                    return;
                }

                args.TokenCache.DeserializeMsalV3(bytes);
            });

            _clientApplication.UserTokenCache.SetAfterAccessAsync(async args =>
            {
                if (!args.HasStateChanged)
                {
                    return;
                }

                var bytes = args.TokenCache.SerializeMsalV3();
                await protectedStorage.SetAsync(TokenCacheKey, bytes).ConfigureAwait(false);
            });
        }

        /// <summary>
        /// Signs in the user.
        /// </summary>
        public async Task SignIn()
        {
            await SignIn(new SignInOptions()).ConfigureAwait(false);
        }

        /// <summary>
        /// Signs in using the specified <see cref="SignInOptions"/>.
        /// </summary>
        /// <param name="signInOptions">The <see cref="SignInOptions"/> to use.</param>
        /// <remarks>This function will apply <see cref="Prompt.ForceLogin"/> because
        /// the Microsoft Identity Platform currently does not support real
        /// logout, so we have to force a login prompt.
        /// </remarks>
        public async Task SignIn(SignInOptions signInOptions)
        {
            await SignIn(c =>
            {
                var builder = c.AcquireTokenInteractive(
                    _currentScopes = signInOptions?.Scopes != null ?
                    _currentScopes.Union(signInOptions.Scopes) :
                    _currentScopes)
                .WithPrompt(Prompt.ForceLogin);

#if MONOANDROID
               builder = builder.WithParentActivityOrWindow(Xamarin.Essentials.Platform.CurrentActivity);
#endif
                return builder;

            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Signs in using the specified Builder function.
        /// </summary>
        /// <typeparam name="T">The type of the Parameter builder.</typeparam>
        /// <param name="builder">
        /// The function that will return a configured parameterbuilder than is ready
        /// for execution.
        /// </param>
        public async Task SignIn<T>(Func<IPublicClientApplication, AbstractPublicClientAcquireTokenParameterBuilder<T>> builder)
            where T : AbstractAcquireTokenParameterBuilder<T>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var acquireTokenBuilder = builder(_clientApplication);
            var result = await acquireTokenBuilder
                .ExecuteAsync().ConfigureAwait(false);

            _currentAccount = result.Account;
            _idToken = new JwtSecurityToken(result.IdToken);
            _currentScopes = result.Scopes;
            await GetUser().ConfigureAwait(false);
        }

        /// <summary>
        /// Signs out the user.
        /// </summary>
        /// <remarks>We only clean the local token cache because Microsoft Identity does not support logout.</remarks>
        public async Task SignOut()
        {
            var accounts = (await _clientApplication.GetAccountsAsync().ConfigureAwait(false)).ToList();

            // clear the cache
            while (accounts.Any())
            {
                await _clientApplication.RemoveAsync(accounts.First()).ConfigureAwait(false);
                accounts = (await _clientApplication.GetAccountsAsync().ConfigureAwait(false)).ToList();
            }

            _currentAccount = null;
            _idToken = null;
            _currentScopes = DefaultScopes;
            await GetUser().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<AccessTokenResult> RequestAccessToken()
        {
            return RequestAccessToken(null);
        }

        /// <inheritdoc />
        public async Task<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
        {
            if (_currentAccount == null)
            {
                var accounts = await _clientApplication.GetAccountsAsync().ConfigureAwait(false);
                if (accounts.Any())
                {
                    _currentAccount = accounts.First();
                }
                else
                {
                    return new AccessTokenResult(this, AccessTokenResultStatus.RequiresRedirect, null);
                }
            }
            AuthenticationResult result = null;
            try
            {
                result = await _clientApplication
                    .AcquireTokenSilent(_currentScopes = options?.Scopes != null ? _currentScopes.Union(options.Scopes) : _currentScopes, _currentAccount)
                    .ExecuteAsync()
                    .ConfigureAwait(false);
            }
            catch (MsalUiRequiredException)
            {
            }

            if (result == null)
            {
                return new AccessTokenResult(this, AccessTokenResultStatus.RequiresRedirect, null);
            }

            if (!string.IsNullOrEmpty(result.IdToken))
            {
                _idToken = new JwtSecurityToken(result.IdToken);
            }

            _currentScopes = result.Scopes;

            return new AccessTokenResult(this, AccessTokenResultStatus.Success, new AccessToken()
            {
                Expires = result.ExpiresOn,
                GrantedScopes = result.Scopes.ToList(),
                Value = result.AccessToken,
            });
        }

        /// <inheritdoc />
        protected async override Task<ClaimsPrincipal> GetAuthenticatedUser()
        {
            var tokenResult = await RequestAccessToken().ConfigureAwait(false);

            if (tokenResult.TryGetToken(out var accessToken))
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Value);

                TAccount account = null;

                if (_currentScopes.Contains("User.Read"))
                {
                    var response = await httpClient.GetAsync(UserInfoEndpoint).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                        account = await JsonSerializer.DeserializeAsync<TAccount>(stream).ConfigureAwait(false);
                    }
                }
                else
                {
                    account = new TAccount();
                }

                AddIdTokenClaimsToAccount(account, _idToken);
                return await AccountClaimsPrincipalFactory.CreateUserAsync(account, Options.UserOptions).ConfigureAwait(false);
            }

            return new ClaimsPrincipal(new ClaimsIdentity());
        }
    }
}
