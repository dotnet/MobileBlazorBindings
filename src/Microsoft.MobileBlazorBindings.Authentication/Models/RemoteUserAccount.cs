// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.MobileBlazorBindings.Authentication
{
    /// <summary>
    /// A user account.
    /// </summary>
    /// <remarks>
    /// The information in this type will be use to produce a <see cref="System.Security.Claims.ClaimsPrincipal"/> for the application.
    /// </remarks>
    public class RemoteUserAccount
    {
        /// <summary>
        /// Gets or sets properties not explicitly mapped about the user.
        /// </summary>
        [JsonExtensionData]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Assigned by JSON serializer.")]
        public IDictionary<string, object> AdditionalProperties { get; set; }
    }
}
