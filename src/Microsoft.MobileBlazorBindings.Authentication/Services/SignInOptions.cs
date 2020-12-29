// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Represents the options for signing in.
    /// </summary>
    public class SignInOptions
    {
        /// <summary>
        /// Gets or sets the list of scopes to additionaly request during sign in.
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }
    }
}
