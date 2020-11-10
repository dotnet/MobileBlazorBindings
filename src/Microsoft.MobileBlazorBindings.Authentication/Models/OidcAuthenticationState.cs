// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    public class OidcAuthenticationState
    {
        //
        // Summary:
        //     Gets or sets the start URL.
        //
        // Value:
        //     The start URL.
        public string StartUrl { get; set; }
        //
        // Summary:
        //     Gets or sets the nonce.
        //
        // Value:
        //     The nonce.
        public string Nonce { get; set; }
        //
        // Summary:
        //     Gets or sets the state.
        //
        // Value:
        //     The state.
        public string State { get; set; }
        //
        // Summary:
        //     Gets or sets the code verifier.
        //
        // Value:
        //     The code verifier.
        public string CodeVerifier { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        public string RedirectUrl { get; set; }
    }
}
