// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using IdentityModel.Client;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Options;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static IdentityModel.OidcClient.OidcClientOptions;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// The default implementation for <see cref="IAuthenticationService"/> that uses IdentityModel.OidcClient to authenticate the user.
    /// </summary>
    /// <typeparam name="TRemoteAuthenticationState">The state to preserve across authentication operations.</typeparam>
    /// <typeparam name="TAccount">The type of the <see cref="RemoteUserAccount" />.</typeparam>
    /// <typeparam name="TProviderOptions">The options to be passed down to the underlying Authentication library handling the authentication operations.</typeparam>
    public abstract class OidcAuthenticationService<TRemoteAuthenticationState, TAccount, TProviderOptions> :
        AuthenticationServiceBase<TAccount, TProviderOptions>,
        IAuthenticationService,
        IAccessTokenProvider
        where TRemoteAuthenticationState : OidcAuthenticationState, new()
        where TProviderOptions : new()
        where TAccount : RemoteUserAccount
    {
        private const string AccessTokenKey = "access_token";
        private const string IdTokenKey = "id_token";

        private readonly IProtectedStorage _protectedStorage;
        private bool _initialized;
        private Task<Task> _initializeTask;
        private Task<Task<AccessTokenResult>> _requestAccessTokenTask;

        private string RefreshTokenKey => $"{GetType().Name}_refresh_token";

        /// <summary>
        /// A cache that caches the requested tokens.
        /// </summary>
        protected ITokenCache TokenCache { get; }

        /// <summary>
        /// The Oidc Client that is used for all requests.
        /// </summary>
        protected OidcClient Client { get; set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options">The options to be passed down to the underlying Authentication library handling the authentication operations.</param>
        /// <param name="tokenCache">The token cache to use to store tokens.</param>
        /// <param name="protectedStorage">The protect storage where refresh tokens will be stored.</param>
        /// <param name="accountClaimsPrincipalFactory">The <see cref="AccountClaimsPrincipalFactory{TAccount}"/> used to generate the <see cref="ClaimsPrincipal"/> for the user.</param>
        protected OidcAuthenticationService(
            IOptionsSnapshot<RemoteAuthenticationOptions<TProviderOptions>> options,
            ITokenCache tokenCache,
            IProtectedStorage protectedStorage,
            AccountClaimsPrincipalFactory<TAccount> accountClaimsPrincipalFactory) : base(options?.Value, accountClaimsPrincipalFactory)
        {
            TokenCache = tokenCache ?? throw new ArgumentNullException(nameof(tokenCache));
            _protectedStorage = protectedStorage;
        }

        /// <inheritdoc />
        public virtual async Task<AccessTokenResult> RequestAccessToken()
        {
            await EnsureAuthService();

            if (await TokenCache.TryGet(AccessTokenKey, out var accessToken))
            {
                return new AccessTokenResult(this, AccessTokenResultStatus.Success, new AccessToken()
                {
                    Expires = accessToken.ValidTo.ToLocalTime(),
                    GrantedScopes = accessToken.Claims.Where(x => x.Type.Equals(Options.UserOptions.ScopeClaim, StringComparison.Ordinal)).Select(x => x.Value).ToList(),
                    Value = accessToken.RawData,
                });
            }

            Task<Task<AccessTokenResult>> requestAccessTokenTask = null;

            // make sure we execute this method only once. We await the task otherwise.
            // With multiple components requesting the authentication state during reload, this happens. Multiple requests would be sent to the authentication
            // service with possible loss of the last refresh/or auth token.
            if ((requestAccessTokenTask = Interlocked.CompareExchange(ref _requestAccessTokenTask, new Task<Task<AccessTokenResult>>(InternalRequestAccessToken), null)) == null)
            {
                requestAccessTokenTask = _requestAccessTokenTask;
                requestAccessTokenTask.Start();
            }

            return await requestAccessTokenTask.Unwrap();

            async Task<AccessTokenResult> InternalRequestAccessToken()
            {
                try
                {
                    string refreshToken = null;

                    if (!string.IsNullOrEmpty(refreshToken = await _protectedStorage.GetAsync<string>(RefreshTokenKey)))
                    {
                        var refreshTokenResult = await Client.RefreshTokenAsync(refreshToken);
                        if (refreshTokenResult.IdentityToken != null)
                        {
                            await TokenCache.Add(IdTokenKey, new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(refreshTokenResult.IdentityToken));
                        }
                        if (refreshTokenResult.RefreshToken != null)
                        {
                            await _protectedStorage.SetAsync(RefreshTokenKey, refreshTokenResult.RefreshToken);
                        }
                        if (refreshTokenResult.AccessToken != null)
                        {
                            accessToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(refreshTokenResult.AccessToken);
                            await TokenCache.Add(AccessTokenKey, accessToken);

                            var grantedScopes = accessToken.Claims.Where(x => x.Type.Equals(Options.UserOptions.ScopeClaim, StringComparison.Ordinal)).Select(x => x.Value).ToList();

                            return new AccessTokenResult(this, AccessTokenResultStatus.Success, new AccessToken()
                            {
                                Expires = accessToken.ValidTo.ToLocalTime(),
                                GrantedScopes = grantedScopes,
                                Value = accessToken.RawData,
                            });
                        }
                    }

                    return new AccessTokenResult(this, AccessTokenResultStatus.RequiresRedirect, null);
                }
                finally
                {
                    _requestAccessTokenTask = null;
                }
            }
        }

        /// <inheritdoc />
        public virtual async Task<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var clientScopes = Client.Options.Scope.Split(' ');

            foreach (var scope in options.Scopes)
            {
                if (!clientScopes.Contains(scope))
                {
                    // unfortunately with the OS primitives and the popup windows, there is no silent
                    // way to acquire a token with an additional scope, so we might as well tell
                    // the application to redo the sign in with the additional scope.
                    return new AccessTokenResult(this, AccessTokenResultStatus.RequiresRedirect, token: null);
                }
            }

            return await RequestAccessToken();
        }

        /// <summary>
        /// Signs in a user.
        /// </summary>
        public async Task SignIn()
        {
            await SignIn(signInOptions: null);
        }

        /// <summary>
        /// Signs in a user using the specified options.
        /// </summary>
        /// <param name="signInOptions">The sign in options to use.</param>
        public async Task SignIn(SignInOptions signInOptions)
        {
            await EnsureAuthService();

            if (signInOptions != null)
            {
                var options = ShallowCopy(Client.Options);

                // merge the scopes for which we already have authorization
                // or the default scopes with the requested scopes.
                var currentScopes = options.Scope.Split(' ');
                var requestedScopes = currentScopes.Union(signInOptions.Scopes);
                options.Scope = string.Join(" ", requestedScopes);

                Client = new OidcClient(options);
            }

            var internalState = await PrepareAuthenticationState();
            var queryString = await StartSecureNavigation(new Uri(internalState.StartUrl), new Uri(internalState.RedirectUrl));
            // no query string on the callback means operation was canceled.
            if (string.IsNullOrEmpty(queryString))
            {
                return;
            }
            await CompleteSignIn(internalState, queryString);
            await GetUser();
        }

        /// <summary>
        /// Prepares the Authentication state for the Login.
        /// </summary>
        protected virtual async Task<TRemoteAuthenticationState> PrepareAuthenticationState()
        {
            var internalState = await Client.PrepareLoginAsync();
            return new TRemoteAuthenticationState()
            {
                StartUrl = internalState.StartUrl,
                CodeVerifier = internalState.CodeVerifier,
                Nonce = internalState.Nonce,
                RedirectUrl = internalState.RedirectUri,
                State = internalState.State,
            };
        }

        /// <summary>
        /// Completes the Sign in process based on the authentication state and the received query string.
        /// </summary>
        /// <param name="authenticationState"></param>
        /// <param name="queryString"></param>
        protected virtual async Task CompleteSignIn(TRemoteAuthenticationState authenticationState, string queryString)
        {
            if (authenticationState is null)
            {
                throw new ArgumentNullException(nameof(authenticationState));
            }

            if (string.IsNullOrEmpty(queryString))
            {
                throw new ArgumentException($"'{nameof(queryString)}' cannot be null or empty", nameof(queryString));
            }

            var internalState = new AuthorizeState()
            {
                CodeVerifier = authenticationState.CodeVerifier,
                Nonce = authenticationState.Nonce,
                RedirectUri = authenticationState.RedirectUrl,
                StartUrl = authenticationState.StartUrl,
                State = authenticationState.State,
            };

            var loginResult = await Client.ProcessResponseAsync(queryString, internalState);

            if (loginResult.IdentityToken != null)
            {
                await TokenCache.Add(IdTokenKey, new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(loginResult.IdentityToken));
            }
            if (loginResult.RefreshToken != null)
            {
                await _protectedStorage.SetAsync(RefreshTokenKey, loginResult.RefreshToken);
            }
            if (loginResult.AccessToken != null)
            {
                await TokenCache.Add(AccessTokenKey, new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(loginResult.AccessToken));
            }
        }

        /// <summary>
        /// Method that is called to Start secure navigation through the platform API to the specified start URL with
        /// an expected return to the return URL.
        /// </summary>
        /// <param name="startUrl">The start URL.</param>
        /// <param name="redirectUrl">The return URL.</param>
        /// <returns></returns>
        protected abstract Task<string> StartSecureNavigation(Uri startUrl, Uri redirectUrl);

        /// <summary>
        /// Creates a new <see cref="OidcClient"/> given the <see cref="OidcProviderOptions"/>.
        /// </summary>
        /// <returns>An <see cref="OidcClient"/> to use.</returns>

        protected virtual async Task<OidcClient> CreateOidcClientFromOptions()
        {
            OidcProviderOptions oidcProviderOptions = null;

            if (Options.ProviderOptions is OidcProviderOptions)
            {
                oidcProviderOptions = Options.ProviderOptions as OidcProviderOptions;
            }
            else if (Options.ProviderOptions is ApiAuthorizationProviderOptions apiAuthorizationProviderOptions)
            {
                var configurationEndpoint = apiAuthorizationProviderOptions.ConfigurationEndpoint;
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(new Uri(configurationEndpoint));

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(
                        $"Got response {response.ReasonPhrase} ({response.StatusCode}) while "
                        + $"requesting API Configuration from {configurationEndpoint}.");
                }

                using var stream = await response.Content.ReadAsStreamAsync();
                oidcProviderOptions = await JsonSerializer.DeserializeAsync<OidcProviderOptions>(stream, options: DeserializationOptions);
            }
            else
            {
                throw new InvalidOperationException($"{typeof(TProviderOptions)} is not a known options type.");
            }

            AuthorizeResponseMode responseMode;

            if (oidcProviderOptions.ResponseMode == null)
            {
                responseMode = AuthorizeResponseMode.Redirect;
            }
            else if (!Enum.TryParse(oidcProviderOptions.ResponseMode, out responseMode))
            {
                throw new OptionsValidationException("ResponseMode", typeof(OidcProviderOptions), new string[] { $"{oidcProviderOptions.ResponseMode} is not a valid response mode." });
            }

            return new OidcClient(new OidcClientOptions()
            {
                Authority = oidcProviderOptions.Authority,
                ClientId = oidcProviderOptions.ClientId,
                PostLogoutRedirectUri = oidcProviderOptions.PostLogoutRedirectUri,
                RedirectUri = oidcProviderOptions.RedirectUri,
                ResponseMode = responseMode,
                LoadProfile = false,
                Scope = string.Join(" ", oidcProviderOptions.DefaultScopes),
            });
        }

        /// <summary>
        /// Signs out a user.
        /// </summary>
        public async virtual Task SignOut()
        {
            await EnsureAuthService();

            string idTokenString = null;
            if (await TokenCache.TryGet(IdTokenKey, out var idToken))
            {
                idTokenString = idToken.RawData;
            }

            var logoutUrl = await Client.PrepareLogoutAsync(new LogoutRequest()
            {
                IdTokenHint = idTokenString,
            });
            await TokenCache.Clear();
            await _protectedStorage.DeleteAsync(RefreshTokenKey);
            await GetUser();

            await StartSecureNavigation(new Uri(logoutUrl), new Uri(Client.Options.PostLogoutRedirectUri));

        }

        /// <summary>
        /// Gets the current authenticated user using JavaScript interop.
        /// </summary>
        /// <returns>A <see cref="Task{ClaimsPrincipal}"/>that will return the current authenticated user when completes.</returns>
        protected override async Task<ClaimsPrincipal> GetAuthenticatedUser()
        {
            var accessTokenResult = await RequestAccessToken();

            if (accessTokenResult.Status == AccessTokenResultStatus.Success && accessTokenResult.TryGetToken(out var accessToken))
            {
                using var userInfoClient = CreateClient(Client.Options);
                using var request = new UserInfoRequest
                {
                    Address = Client.Options.ProviderInformation.UserInfoEndpoint,
                    Token = accessToken.Value,
                };

                var userInfoResponse = await userInfoClient.GetUserInfoAsync(request).ConfigureAwait(true);

                if (userInfoResponse.Exception != null)
                {
                    throw userInfoResponse.Exception;
                }

                var account = JsonSerializer.Deserialize<TAccount>(userInfoResponse.Raw);

                await MergeIdTokenClaims(account);

                return await AccountClaimsPrincipalFactory.CreateUserAsync(account, Options.UserOptions);
            }
            else
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }
        }

        private async Task MergeIdTokenClaims(TAccount account)
        {
            if (await TokenCache.TryGet(IdTokenKey, out var idToken))
            {
                AddIdTokenClaimsToAccount(account, idToken);
            }
        }

        /// <summary>
        /// Ensures that the Authentication Service is Initialized.
        /// </summary>
        protected virtual async Task EnsureAuthService()
        {
            if (!_initialized)
            {
                try
                {
                    Task<Task> initializeTask;
                    if ((initializeTask = Interlocked.CompareExchange(ref _initializeTask, new Task<Task>(async () => Client = await CreateOidcClientFromOptions()), null)) == null)
                    {
                        _initializeTask.Start();
                        await _initializeTask.Unwrap();
                        _initialized = true;
                    }
                    else
                    {
                        await initializeTask.Unwrap();
                    }
                }
                finally
                {
                    _initializeTask = null;
                }
            }
        }

        private static HttpClient CreateClient(OidcClientOptions options)
        {
            HttpClient client;

            if (options.BackchannelHandler != null)
            {
                client = new HttpClient(options.BackchannelHandler);
            }
            else
            {
                client = new HttpClient();
            }

            client.Timeout = options.BackchannelTimeout;
            return client;
        }

        private static TObject ShallowCopy<TObject>(TObject obj)
            where TObject : new()
        {
            var result = new TObject();

            foreach (var property in typeof(TObject).GetProperties())
            {
                if (!property.CanWrite || !property.CanRead)
                {
                    continue;
                }
                property.SetValue(result, property.GetValue(obj));
            }

            return result;
        }

        private static JsonSerializerOptions DeserializationOptions
        {
            get
            {
                var result = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                result.Converters.Add(new ScopeConverter());

                return result;
            }
        }
    }
}
