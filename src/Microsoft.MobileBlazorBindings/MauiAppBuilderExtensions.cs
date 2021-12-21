using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using System;

namespace Microsoft.MobileBlazorBindings
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder UseMobileBlazorBindings(this MauiAppBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services
                .AddSingleton<ShellNavigationManager>()
                .AddScoped<MobileBlazorBindingsRenderer>();

            return builder;
        }
    }
}
