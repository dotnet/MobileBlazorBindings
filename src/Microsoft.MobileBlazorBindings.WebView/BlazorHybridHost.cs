// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Microsoft.MobileBlazorBindings.WebView
{
    public static class BlazorHybridHost
    {
        private static readonly List<Lazy<IFileProvider>> _resourceFileProviders = new List<Lazy<IFileProvider>>();

        /// <summary>
        /// Adds the specified resources to the list of assemblies that contain static web resources for the
        /// web portion of the application. The resources must be embedded in an assembly that directly
        /// references the <c>Microsoft.Extensions.FileProviders.Embedded</c> NuGet package and specifies
        /// <c>&lt;GenerateEmbeddedFilesManifest&gt;true&lt;/GenerateEmbeddedFilesManifest&gt;</c>
        /// in the project settings.
        /// </summary>
        /// <param name="resourceAssembly">An assembly that contains static web assets as embedded resources.</param>
        /// <param name="contentRoot">The root folder of the content in the application. This is prepended to filenames to get the full path.</param>
        public static void AddResourceAssembly(Assembly resourceAssembly, string contentRoot)
        {
            if (resourceAssembly is null)
            {
                throw new ArgumentNullException(nameof(resourceAssembly));
            }

            _resourceFileProviders.Add(
                new Lazy<IFileProvider>(
                    valueFactory: () => new ManifestEmbeddedFileProvider(resourceAssembly, contentRoot),
                    isThreadSafe: true));
        }

        /// <summary>
        /// Tries to load the given <paramref name="filename"/> from a registered resource assembly.
        /// Resource assemblies are registered by calling <see cref="AddResourceAssembly(Assembly, string)"/>.
        /// </summary>
        /// <param name="filename">The filename of the resource to load. This is appended to the specified content root in a resource assembly to get the full path.</param>
        /// <param name="fileStream">A readable stream with the resource content. Only valid if the method call returns <c>true</c>.</param>
        /// <returns>Returns <c>true</c> if the resource file was found; <c>false</c> otherwise.</returns>
        public static bool TryGetEmbeddedResourceFile(string filename, out Stream fileStream)
        {
            foreach (var resourceFileProviderCreator in _resourceFileProviders)
            {
                var resourceFileProvider = resourceFileProviderCreator.Value;
                var fileInfo = resourceFileProvider.GetFileInfo(filename);
                if (fileInfo.Exists)
                {
                    fileStream = fileInfo.CreateReadStream();
                    return true;
                }
            }
            fileStream = null;
            return false;
        }
    }
}
