// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public static class BlazorWebHost
    {
        public static IHostBuilder CreateDefaultBuilder(string[] args = null)
        {
            // Inspired by Microsoft.Extensions.Hosting.Host, which can be seen here:
            // https://github.com/dotnet/runtime/blob/master/src/libraries/Microsoft.Extensions.Hosting/src/Host.cs
            // But slightly modified to work on all of Android, iOS, and UWP.

            var builder = new HostBuilder();

            builder.UseContentRoot(Directory.GetCurrentDirectory());
            builder.UseWebRoot("wwwroot");
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

                if (env.IsDevelopment())
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true);
                    }
                }

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            });
            builder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConsole(configure => configure.DisableColors = true);
                logging.AddDebug();
                logging.AddEventSourceLogger();
            })
            .UseDefaultServiceProvider((context, options) =>
            {
                var isDevelopment = context.HostingEnvironment.IsDevelopment();
                options.ValidateScopes = isDevelopment;
                options.ValidateOnBuild = isDevelopment;
            });

            return builder;
        }
    }

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
    }
}
