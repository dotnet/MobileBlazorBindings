using Microsoft.MobileBlazorBindings.WebView;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;

namespace HybridApp
{
    public class App : Application
    {
        public App()
        {
            BlazorDesktopHost.AddResourceAssembly(GetType().Assembly, contentRoot: "WebUI/wwwroot");

            var host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorDesktop();

                    // Register app-specific services
                    services.AddSingleton<CounterState>();
                })
                .Build();

            MainPage = new ContentPage { Title = "My Application" };
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
