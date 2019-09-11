using Xamarin.Forms;
using BlaxamarinSample.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blaxamarin.Framework;

namespace BlaxamarinSample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            var host = Host.CreateDefaultBuilder()
                //.AddBlinForms()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    services.AddSingleton<AppState>();
                })
                .Build();

            MainPage = host.Services.GetComponentContentPage<TodoApp>();
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
