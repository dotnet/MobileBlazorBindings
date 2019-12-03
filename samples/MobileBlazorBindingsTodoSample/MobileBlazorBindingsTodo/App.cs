using Microsoft.MobileBlazorBindings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Forms;

namespace MobileBlazorBindingsTodo
{
    public class App : Application
    {
        public IHost EmblazonHost { get; }

        public App(IServiceCollection additionalServices)
        {
            EmblazonHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register backend-specific services (e.g. iOS, Android)
                    if (additionalServices != null)
                    {
                        services.AddAdditionalServices(additionalServices);
                    }

                    // Register app-specific services
                    services.AddSingleton<AppState>();
                })
                .Build();

            EmblazonHost.AddComponent<TodoApp>(parent: this);
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
