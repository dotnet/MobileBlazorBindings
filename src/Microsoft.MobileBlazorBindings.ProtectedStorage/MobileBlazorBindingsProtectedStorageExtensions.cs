// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using System;
using Xamarin.Forms;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods to add protected storage services for all supported Mobile Blazor Bindings platforms.
    /// </summary>
    public static class MobileBlazorBindingsProtectedStorageExtensions
    {
        /// <summary>
        /// Adds protected storage to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance to add the services to.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddProtectedStorage(this IServiceCollection services)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.macOS:
                case Device.Android:
                case Device.Tizen:
                    services.TryAddSingleton<IProtectedStorage, XamarinProtectedStorage>();
                    break;
                case Device.WPF:
                    services.TryAddSingleton<IProtectedStorage, WindowsProtectedStorage>();
                    break;
                default:
                    throw new PlatformNotSupportedException($"Platform {Device.RuntimePlatform} is not supported by {ThisAssembly.AssemblyName}");
            }

            return services;
        }
    }
}
