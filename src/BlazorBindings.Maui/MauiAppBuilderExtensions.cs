// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using System;

namespace BlazorBindings.Maui
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder UseMauiBlazorBindings(this MauiAppBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services
                .AddSingleton<ShellNavigationManager>()
                .AddScoped<MauiBlazorBindingsRenderer>();

            return builder;
        }
    }
}
