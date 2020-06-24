using Microsoft.MobileBlazorBindings.WebView.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlazorDesktopServiceCollectionExtensions
    {
        public static void AddBlazorDesktop(this IServiceCollection services)
        {
            services.AddScoped<NavigationManager, DesktopNavigationManager>();
            services.AddScoped<INavigationInterception, DesktopNavigationInterception>();
        }
    }
}
