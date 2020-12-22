// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.MobileBlazorBindings.Authentication.Internal
{
    /// <summary>
    /// This is an internal API that supports the Microsoft.MobileBlazorBindings.Authentication
    /// infrastructure and not subject to the same compatibility standards as public APIs.
    /// It may be changed or removed without notice in any release.
    /// </summary>
    internal interface IRemoteAuthenticationPathsProvider
    {
        /// <summary>
        /// This is an internal API that supports the Microsoft.MobileBlazorBindings.Authentication
        /// infrastructure and not subject to the same compatibility standards as public APIs.
        /// It may be changed or removed without notice in any release.
        /// </summary>
        RemoteAuthenticationApplicationPathsOptions ApplicationPaths { get; }
    }
}
