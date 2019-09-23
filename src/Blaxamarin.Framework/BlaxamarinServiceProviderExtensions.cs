using Blaxamarin.Framework.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public static class BlaxamarinServiceProviderExtensions
    {
        /// <summary>
        /// Creates a component of type <typeparamref name="TComponent"/> and adds it as a child of <paramref name="parent"/>.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="services"></param>
        /// <param name="parent"></param>
        public static void AddComponent<TComponent>(this IServiceProvider services, XF.Element parent) where TComponent : IComponent
        {
            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            var renderer = new BlaxamarinRenderer(services, services.GetRequiredService<ILoggerFactory>());
            renderer.Dispatcher.InvokeAsync(async () =>
            {
                await renderer.AddComponent<TComponent>(new ElementHandler(renderer, parent));
            });
        }
    }
}
