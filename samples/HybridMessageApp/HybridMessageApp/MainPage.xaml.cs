using HybridMessageApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings.WebView;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HybridMessageApp
{
    public partial class MainPage : Application
    {
        public MainPage()
        {
            BlazorHybridHost.AddResourceAssembly(GetType().Assembly, contentRoot: "WebUI/wwwroot");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorHybrid();
            serviceCollection.AddLogging();
            serviceCollection.AddSingleton<AppState>();
            BlazorHybridDefaultServices.Instance = serviceCollection.BuildServiceProvider();

            InitializeComponent();

            MasterDetails.IsPresented = false;
            WorkaroundDisplayIssue();
        }

        private async void WorkaroundDisplayIssue()
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
