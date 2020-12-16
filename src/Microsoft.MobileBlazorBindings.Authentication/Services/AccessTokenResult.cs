// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Represents the result of trying to provision an access token.
    /// </summary>
    public class AccessTokenResult
    {
        private readonly AccessToken _token;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of <see cref="AccessTokenResult"/>.
        /// </summary>
        /// <param name="authenticationService">The authentication service to use.</param>
        /// <param name="status">The status of the result.</param>
        /// <param name="token">The <see cref="AccessToken"/> in case it was successful.</param>
        public AccessTokenResult(IAuthenticationService authenticationService, AccessTokenResultStatus status, AccessToken token)
        {
            Status = status;
            _token = token;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Gets or sets the status of the current operation. See <see cref="AccessTokenResultStatus"/> for a list of statuses.
        /// </summary>
        public AccessTokenResultStatus Status { get; }

        /// <summary>
        /// Determines whether the token request was successful and makes the <see cref="AccessToken"/> available for use when it is.
        /// </summary>
        /// <param name="accessToken">The <see cref="AccessToken"/> if the request was successful.</param>
        /// <returns><c>true</c> when the token request is successful; <c>false</c> otherwise.</returns>
        public bool TryGetToken(out AccessToken accessToken)
        {
            if (Status == AccessTokenResultStatus.Success)
            {
                accessToken = _token;
                return true;
            }
            else
            {
                accessToken = null;
                return false;
            }
        }

        /// <summary>
        /// Tries to request permission to obtain the additional scopes.
        /// </summary>
        /// <param name="scopes">The additional scopes.</param>
        /// <returns></returns>
        public async Task RequestPermission(IEnumerable<string> scopes)
        {
            await _authenticationService.SignIn(new SignInOptions()
            {
                Scopes = scopes,
            });
        }
    }
}
