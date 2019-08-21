using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    public static class Blaxamarin
    {
        public static void Run<T>(Application app) where T : IComponent
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var renderer = new BlaxamarinRenderer(serviceProvider, app);
            renderer.Dispatcher.InvokeAsync(async () =>
            {
                await renderer.AddComponent<T>();

                // TODO: Do we need anything similar to what BlinForms does?
                //Application.Run(renderer.RootForm);
            });
        }
    }
}
