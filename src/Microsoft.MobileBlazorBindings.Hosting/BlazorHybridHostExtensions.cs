// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Microsoft.MobileBlazorBindings
{
    public static class BlazorHybridHostExtensions
    {
        /// <summary>
        /// Specify the webroot directory to be used by the hybrid host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="webRoot">Path to the root directory used by the hybrid host.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder UseWebRoot(this IHostBuilder hostBuilder, string webRoot)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (string.IsNullOrEmpty(webRoot))
            {
                throw new ArgumentException($"'{nameof(webRoot)}' cannot be null or empty", nameof(webRoot));
            }

            return hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                configurationBuilder.Properties[BlazorHybridDefaults.WebRootKey] = webRoot;
            });
        }

        /// <summary>
        /// Enables static file serving for the current request path
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder UseStaticFiles(this IHostBuilder hostBuilder)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            IFileProvider rootProvider = null;

            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                if (!configurationBuilder.Properties.TryGetValue(BlazorHybridDefaults.WebRootKey, out object webRoot))
                {
                    throw new KeyNotFoundException("WebRoot not set.");
                }

                rootProvider = new PhysicalFileProvider(
                    Path.Combine(hostBuilderContext.HostingEnvironment.ContentRootPath, (string)webRoot),
                    Extensions.FileProviders.Physical.ExclusionFilters.Sensitive);

                configurationBuilder.Properties[BlazorHybridDefaults.WebRootFileProvider] = rootProvider;
            });

            return hostBuilder.ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddSingleton<IFileProvider>(rootProvider);
            });
        }

        /// <summary>
        /// Enables static file serving for the current request path.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="fileProvider">The file provider to use.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder UseStaticFiles(this IHostBuilder hostBuilder, IFileProvider fileProvider)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            IFileProvider rootProvider = null;

            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                rootProvider = fileProvider;

                configurationBuilder.Properties[BlazorHybridDefaults.WebRootFileProvider] = fileProvider;
            });

            return hostBuilder.ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddSingleton<IFileProvider>(rootProvider);
            });
        }

        /// <summary>
        /// Enables static file serving using the resource assembly that contains static web resources for the
        /// web portion of the application. The resources must be embedded in an assembly that directly
        /// references the <c>Microsoft.Extensions.FileProviders.Embedded</c> NuGet package and specifies
        /// <c>&lt;GenerateEmbeddedFilesManifest&gt;true&lt;/GenerateEmbeddedFilesManifest&gt;</c>
        /// in the project settings.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="resourceAssembly">An assembly that contains static web assets as embedded resources.</param>
        public static IHostBuilder UseResourceAssembly(this IHostBuilder hostBuilder, Assembly resourceAssembly)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            IFileProvider rootProvider = null;

            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                if (!configurationBuilder.Properties.TryGetValue(BlazorHybridDefaults.WebRootKey, out object webRoot))
                {
                    throw new KeyNotFoundException("WebRoot not set.");
                }

                rootProvider = new ManifestEmbeddedFileProvider(resourceAssembly, (string)webRoot);

                configurationBuilder.Properties[BlazorHybridDefaults.WebRootFileProvider] = rootProvider;
            });

            return hostBuilder.ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddSingleton<IFileProvider>(rootProvider);
            });
        }
    }
}
