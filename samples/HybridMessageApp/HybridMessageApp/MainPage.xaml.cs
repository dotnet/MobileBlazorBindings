using BlazorDesktop;
using MessageApp.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MessageApp
{
    public partial class MainPage : Application
    {
        public MainPage()
        {
            BlazorDesktopHost.AddResourceAssembly(GetType().Assembly, contentRoot: "WebUI/wwwroot");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorDesktop();
            serviceCollection.AddLogging();
            serviceCollection.AddSingleton<AppState>();
            BlazorDesktopDefaultServices.Instance = serviceCollection.BuildServiceProvider();

            InitializeComponent();

            MasterDetails.IsPresented = false;
            WorkaroundDisplayIssue();
        }

        async void WorkaroundDisplayIssue()
        {
            await Task.Delay(1000);
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                MasterDetails.IsPresented = false;
            });
            await Task.Delay(1);
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                MasterDetails.IsPresented = true;
            });
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
