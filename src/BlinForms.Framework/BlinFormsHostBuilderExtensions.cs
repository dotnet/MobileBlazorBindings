using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BlinForms.Framework
{
    public static class BlinFormsHostBuilderExtensions
    {
        /// <summary>
        /// Registers <see cref="BlinFormsHostedService"/> in the DI container. Call this as part of configuring the
        /// host to enable BlinForms.
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder AddBlinForms(this IHostBuilder hostBuilder)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<BlinFormsHostedService>();
            });

            return hostBuilder;
        }
    }
}
