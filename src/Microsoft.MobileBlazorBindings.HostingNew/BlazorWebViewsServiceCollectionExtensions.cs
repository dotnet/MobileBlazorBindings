using Microsoft.JSInterop;
using Microsoft.MobileBlazorBindings.HostingNew;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlazorWebViewsServiceCollectionExtensions
    {
        public static void AddBlazorWebViews(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging();
            serviceCollection.AddScoped<IJSRuntime, BlazorWebViewJSRuntime>();
        }
    }
}
