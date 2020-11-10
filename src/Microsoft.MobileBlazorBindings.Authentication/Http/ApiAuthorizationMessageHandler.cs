// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// A <see cref="DelegatingHandler"/> that attaches access tokens to outgoing <see cref="HttpRequestMessage"/> instances.
    /// Access tokens will only be added when the request URI is within the API's base URI.
    /// </summary>
    /// <remarks>
    /// Since message handlers have their own scope to resolve from, <see cref="RegisterTokenProvider(string, IAccessTokenProvider)"/>
    /// needs to be called to register the correct <see cref="IAccessTokenProvider"/> for the provided base URL.
    /// </remarks>
    public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApiAuthorizationMessageHandler"/>.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API.</param>
        public ApiAuthorizationMessageHandler(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new System.ArgumentException($"'{nameof(baseUrl)}' cannot be null or empty", nameof(baseUrl));
            }

            ConfigureHandler(new string[] { baseUrl });
        }

        /// <summary>
        /// Registers a token provider for the specified urls.
        /// </summary>
        /// <param name="baseUrl">The base URL of the API.</param>
        /// <param name="accessTokenProvider">The <see cref="IAccessTokenProvider"/> instance to use.</param>
        public static void RegisterTokenProvider(string baseUrl, IAccessTokenProvider accessTokenProvider)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new System.ArgumentException($"'{nameof(baseUrl)}' cannot be null or empty", nameof(baseUrl));
            }

            RegisterTokenProvider(new string[] { baseUrl }, accessTokenProvider);
        }
    }
}
