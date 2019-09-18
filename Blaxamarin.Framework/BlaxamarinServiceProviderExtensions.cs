using Blaxamarin.Framework.Elements;
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
                await renderer.AddComponent<TComponent>(new ElementWrapper(parent));
            });
        }

        private sealed class ElementWrapper : IFormsControlHandler
        {
            public ElementWrapper(Xamarin.Forms.Element element)
            {
                ElementControl = element ?? throw new ArgumentNullException(nameof(element));
            }

            public XF.Element ElementControl { get; }
            public object NativeControl => ElementControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                Element.ApplyAttribute(ElementControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
