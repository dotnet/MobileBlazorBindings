// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.MobileBlazorBindings.ProtectedStorage;

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
#if WINDOWS
            services.TryAddSingleton<IProtectedStorage, WindowsProtectedStorage>();
#else
            services.TryAddSingleton<IProtectedStorage, MauiProtectedStorage>();
#endif
            return services;
        }
    }
}
