using Blaxamarin.Framework.Elements.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public static class BlaxamarinHostExtensions
    {
        /// <summary>
        /// Creates a component of type <typeparamref name="TComponent"/> and adds it as a child of <paramref name="parent"/>.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="host"></param>
        /// <param name="parent"></param>
        public static async Task AddComponent<TComponent>(this IHost host, XF.Element parent) where TComponent : IComponent
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            if (parent is null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            var services = host.Services;
            var renderer = new BlaxamarinRenderer(services, services.GetRequiredService<ILoggerFactory>());
            await renderer.AddComponent<TComponent>(new ElementHandler(renderer, parent)).ConfigureAwait(false);
        }
    }
}
