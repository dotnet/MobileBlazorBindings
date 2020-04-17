using BlazorDesktop;
using MessageApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace MessageApp
{
    public partial class MainPage : Application
    {
        public MainPage()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorDesktop();
            serviceCollection.AddLogging();
            serviceCollection.AddSingleton<AppState>();
            BlazorDesktopDefaultServices.Instance = serviceCollection.BuildServiceProvider();

            InitializeComponent();
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
