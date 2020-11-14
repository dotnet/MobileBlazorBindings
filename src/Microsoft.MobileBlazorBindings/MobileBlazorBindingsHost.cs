// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.MobileBlazorBindings
{
    public static class MobileBlazorBindingsHost
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

            EnableStyleSheetSupport();

            return builder;
        }

        private static void EnableStyleSheetSupport()
        {
            // Force the Xamarin.Forms.Xaml assembly to load, which will cause the style sheet dependency
            // to be made available to Xamarin.Forms' Dependency Injection service. DI services are
            // made available by scanning loaded assemblies for various attributes, but sometimes this
            // assembly isn't yet loaded, meaning the CSS StyleSheet loader service won't be available
            // to be discovered.
            // See these issues for more info:
            // - Xamarin.Forms issue: https://github.com/xamarin/Xamarin.Forms/issues/7775
            // - MBB issue with previous fix: https://github.com/xamarin/MobileBlazorBindings/issues/82#issuecomment-618997676
            // - MBB issue with updated fix: https://github.com/xamarin/MobileBlazorBindings/issues/140

            // Note: This code is somewhat arbitrary. The only requirement is that it *runs* code in a type
            // that's in the Xamarin.Forms.Xaml assembly.
            // Note: There are many types in the Xamarin.Forms.Xaml namespace that are *not* in that
            // assembly, so be careful if changing this.
            _ = new Xamarin.Forms.Xaml.XamlCompilationAttribute(Xamarin.Forms.Xaml.XamlCompilationOptions.Skip);
        }
    }
}
