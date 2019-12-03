using Microsoft.Blazor.Native;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Forms;

namespace MobileBlazorBindingsWeather
{
    public class App : Application
    {
        public App()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    services.AddSingleton<WeatherService>();
                })
                .Build();

            host.AddComponent<MainPage>(parent: this);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
