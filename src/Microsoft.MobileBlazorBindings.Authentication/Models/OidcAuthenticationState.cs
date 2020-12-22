// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// State that is maintained between the Oidc Authorization call and the callback.
    /// </summary>
    public class OidcAuthenticationState
    {
        /// <summary>
        /// Gets or sets the start URL
        /// </summary>
        public string StartUrl { get; set; }

        /// <summary>
        /// Gets or sets the nonce.
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the code verifier.
        /// </summary>
        public string CodeVerifier { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}
