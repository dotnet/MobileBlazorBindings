using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public static class BlaxamarinServiceProviderExtensions
    {
        public static ContentPage GetComponentContentPage<TComponent>(this IServiceProvider services) where TComponent : IComponent
        {
            var renderer = new BlaxamarinRenderer(services, services.GetRequiredService<ILoggerFactory>());
            var result = renderer.Dispatcher.InvokeAsync(async () =>
            {
                await renderer.AddComponent<TComponent>();

                return renderer.ContentPage;
            });
            return result.GetAwaiter().GetResult();
        }
    }
}
