using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlazorWebViewsServiceCollectionExtensions
    {
        public static void AddBlazorWebViews(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging();
        }
    }
}
