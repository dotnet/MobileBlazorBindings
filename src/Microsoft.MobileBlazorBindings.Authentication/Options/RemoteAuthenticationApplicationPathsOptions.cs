// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// Represents the options for the paths used by the application for authentication operations. These paths are relative to the base.
    /// </summary>
    public class RemoteAuthenticationApplicationPathsOptions
    {
        /// <summary>
        /// Gets or sets the remote path to the remote endpoint for registering new users.
        /// It might be absolute and point outside of the application.
        /// </summary>
        public string RemoteRegisterPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the remote endpoint for modifying the settings for the user profile.
        /// It might be absolute and point outside of the application.
        /// </summary>
        public string RemoteProfilePath { get; set; }
    }
}
