using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;

namespace MyApplication
{
    public class App : Application
    {
        public App()
        {
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorDesktop();

                    // Register app-specific services
                    services.AddSingleton<CounterState>();
                })
                .Build();

            MainPage = new ContentPage();
            host.AddComponent<Main>(parent: MainPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
