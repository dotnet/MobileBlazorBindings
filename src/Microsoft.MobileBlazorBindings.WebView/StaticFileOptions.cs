// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace Microsoft.MobileBlazorBindings
{
    /// <summary>
    /// Options for serving static files
    /// </summary>
    public class StaticFileOptions
    {
        /// <summary>
        /// The file system used to locate resources
        /// </summary>
        public IFileProvider FileProvider { get; set; }
    }
}