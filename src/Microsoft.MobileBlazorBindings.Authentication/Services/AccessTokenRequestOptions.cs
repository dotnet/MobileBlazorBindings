// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Represents the options for provisioning an access token on behalf of a user.
    /// </summary>
    public class AccessTokenRequestOptions
    {
        /// <summary>
        /// Gets or sets the list of scopes to request for the token.
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }
    }
}
