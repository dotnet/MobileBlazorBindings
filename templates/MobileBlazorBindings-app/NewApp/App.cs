using System;
using Microsoft.Blazor.Native;
using Microsoft.Extensions.Hosting;
using Xamarin.Forms;

namespace NewApp
{
    public class App : Application
    {
        public App()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    //services.AddSingleton<AppState>();
                })
                .Build();

            host.AddComponent<HelloWorld>(parent: this);
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
