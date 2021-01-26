// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Microsoft.MobileBlazorBindings.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlazorHybridServiceCollectionExtensions
    {
        public static void AddBlazorHybrid(this IServiceCollection services)
        {
            services.AddScoped<NavigationManager, BlazorHybridNavigationManager>();
            services.AddScoped<INavigationInterception, BlazorHybridNavigationInterception>();

            services.AddScoped<IJSRuntime, BlazorHybridJSRuntime>();
        }
    }
}
