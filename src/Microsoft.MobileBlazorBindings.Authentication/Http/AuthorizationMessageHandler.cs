// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// A <see cref="DelegatingHandler"/> that attaches access tokens to outgoing <see cref="HttpResponseMessage"/> instances.
    /// Access tokens will only be added when the request URI is within one of the base addresses configured using
    /// <see cref="ConfigureHandler(IEnumerable{string}, IEnumerable{string})"/>.
    /// </summary>
    /// <remarks>
    /// Since message handlers have their own scope to resolve from, <see cref="RegisterTokenProvider(IEnumerable{string}, IAccessTokenProvider)"/>
    /// needs to be called to register the correct <see cref="IAccessTokenProvider"/> for the provided base URL.
    /// </remarks>
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private static readonly ConcurrentDictionary<string, IAccessTokenProvider> TokenProviders = new ConcurrentDictionary<string, IAccessTokenProvider>();

        private Uri[] _authorizedUris;
        private AccessTokenRequestOptions _tokenOptions;

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var now = DateTimeOffset.Now;
            if (_authorizedUris == null)
            {
                throw new InvalidOperationException($"The '{nameof(AuthorizationMessageHandler)}' is not configured. " +
                    $"Call '{nameof(AuthorizationMessageHandler.ConfigureHandler)}' and provide a list of endpoint URLs to attach the token to.");
            }

            var authorizedUri = _authorizedUris.SingleOrDefault(uri => uri.IsBaseOf(request.RequestUri));

            if (authorizedUri != null)
            {
                var key = authorizedUri.ToString();

                if (!TokenProviders.TryGetValue(key, out var tokenProvider))
                {
                    throw new HttpRequestException($"No token provider registered for URL {authorizedUri}");
                }

                var tokenResult = _tokenOptions != null ?
                    await tokenProvider.RequestAccessToken(_tokenOptions) :
                    await tokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
                }
                else
                {
                    throw new AccessTokenNotAvailableException(tokenResult, _tokenOptions?.Scopes);
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Configures this handler to authorize outbound HTTP requests using an access token. The access token is only attached if at least one of
        /// <paramref name="authorizedUrls" /> is a base of <see cref="HttpRequestMessage.RequestUri" />.
        /// </summary>
        /// <param name="authorizedUrls">The base addresses of endpoint URLs to which the token will be attached.</param>
        /// <param name="scopes">The list of scopes to use when requesting an access token.</param>
        /// identity provider is necessary.
        /// <returns>This <see cref="AuthorizationMessageHandler"/>.</returns>
        public AuthorizationMessageHandler ConfigureHandler(
            IEnumerable<string> authorizedUrls,
            IEnumerable<string> scopes = null)
        {
            if (_authorizedUris != null)
            {
                throw new InvalidOperationException("Handler already configured.");
            }
            if (authorizedUrls == null)
            {
                throw new ArgumentNullException(nameof(authorizedUrls));
            }

            var uris = authorizedUrls.Select(uri => new Uri(uri, UriKind.Absolute)).ToArray();
            if (uris.Length == 0)
            {
                throw new ArgumentException("At least one URL must be configured.", nameof(authorizedUrls));
            }

            _authorizedUris = uris;
            var scopesList = scopes?.ToArray();
            if (scopesList != null)
            {
                _tokenOptions = new AccessTokenRequestOptions
                {
                    Scopes = scopesList,
                };
            }

            return this;
        }

        /// <summary>
        /// Registers a token provider for the specified URLs.
        /// </summary>
        /// <param name="authorizedUrls">The URLs to register a token provider for.</param>
        /// <param name="accessTokenProvider">The <see cref="IAccessTokenProvider"/> instance to use.</param>
        public static void RegisterTokenProvider(IEnumerable<string> authorizedUrls, IAccessTokenProvider accessTokenProvider)
        {
            if (authorizedUrls is null)
            {
                throw new ArgumentNullException(nameof(authorizedUrls));
            }
            if (accessTokenProvider is null)
            {
                throw new ArgumentNullException(nameof(accessTokenProvider));
            }

            foreach (var authorizedUrl in authorizedUrls)
            {
                TokenProviders.AddOrUpdate(authorizedUrl, accessTokenProvider, (key, oldValue) => accessTokenProvider);
            }
        }
    }
}
