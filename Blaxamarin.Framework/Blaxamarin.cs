using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public static class Blaxamarin
    {
        public static ContentPage Run<T>() where T : IComponent
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var renderer = new BlaxamarinRenderer(serviceProvider, new LoggerFactory());
            var result = renderer.Dispatcher.InvokeAsync(async () =>
            {
                await renderer.AddComponent<T>();

                // TODO: Do we need anything similar to what BlinForms does?
                //Application.Run(renderer.RootForm);

                return renderer.ContentPage;
            });
            return result.GetAwaiter().GetResult();
        }
    }
}
