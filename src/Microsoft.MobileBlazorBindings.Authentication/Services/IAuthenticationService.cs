// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Represents a contract for services that perform authentication operations for a Mobile Blazor Bindings application.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Signs in a user.
        /// </summary>
        Task SignIn();

        /// <summary>
        /// Signs in a user, using the specified options.
        /// </summary>
        /// <param name="signInOptions">The sign in options to use.</param>
        Task SignIn(SignInOptions signInOptions);

        /// <summary>
        /// Signs out a user.
        /// </summary>
        Task SignOut();
    }
}
