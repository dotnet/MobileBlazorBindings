using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    public static class BlinForms
    {
        public static void Run<T>() where T : IComponent
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var renderer = new BlinFormsRenderer(serviceProvider);
            renderer.Dispatcher.InvokeAsync(() =>
            {
                renderer.AddComponent<T>();
                Application.Run(renderer.RootForm);
            });
        }
    }
}
