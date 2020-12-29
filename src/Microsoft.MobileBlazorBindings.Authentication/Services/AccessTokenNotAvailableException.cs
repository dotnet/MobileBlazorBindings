// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// An <see cref="Exception"/> that is thrown when an <see cref="AuthorizationMessageHandler"/> instance
    /// is not able to provision an access token.
    /// </summary>
#pragma warning disable CA1032 // Implement standard exception constructors
    public class AccessTokenNotAvailableException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        private readonly AccessTokenResult _tokenResult;
        private readonly IEnumerable<string> _scopes;

        /// <summary>
        /// Initialize a new instance of <see cref="AccessTokenNotAvailableException"/>.
        /// </summary>
        /// <param name="tokenResult">The <see cref="AccessTokenResult"/>.</param>
        /// <param name="scopes">The scopes.</param>
        public AccessTokenNotAvailableException(
            AccessTokenResult tokenResult,
            IEnumerable<string> scopes)
            : base(message: "Unable to provision an access token for the requested scopes: " +
                  scopes != null ? $"'{string.Join(", ", scopes ?? Array.Empty<string>())}'" : "(default scopes)")
        {
            _tokenResult = tokenResult;
            _scopes = scopes;
        }

        /// <summary>
        /// Requests permission for receiving the access token.
        /// </summary>
        public async Task RequestPermission() => await _tokenResult.RequestPermission(_scopes);
    }
}
