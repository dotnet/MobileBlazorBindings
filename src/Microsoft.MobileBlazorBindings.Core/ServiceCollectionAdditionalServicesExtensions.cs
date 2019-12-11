// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionAdditionalServicesExtensions
    {
        /// <summary>
        /// Copies service descriptors from one service collection to another.
        /// </summary>
        /// <param name="services">The destination service collection to which the additional services will be added.</param>
        /// <param name="additionalServices">The list of additional services to add.</param>
        /// <returns></returns>
        public static IServiceCollection AddAdditionalServices(this IServiceCollection services, IServiceCollection additionalServices)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (additionalServices is null)
            {
                throw new ArgumentNullException(nameof(additionalServices));
            }

            foreach (var additionalService in additionalServices)
            {
                services.Add(additionalService);
            }
            return services;
        }
    }
}
