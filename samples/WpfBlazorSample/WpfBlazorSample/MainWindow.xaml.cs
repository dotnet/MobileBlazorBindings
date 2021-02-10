using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings;
using Microsoft.MobileBlazorBindings.Hosting;
using RazorClassLibrarySample;
using System.Windows;

namespace WpfBlazorSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppState _appState;

        public MainWindow()
        {
            InitializeComponent();

            MyBlazorWebView.ComponentType = typeof(RazorClassLibrarySample.SampleWebComponent);

            var hostBuilder = BlazorWebHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorHybrid();

                    // Register app-specific services
                    services.AddSingleton<AppState>();
                })
                .UseWebRoot("wwwroot");

            hostBuilder.UseStaticFiles();

            var host = hostBuilder.Build();

            _appState = host.Services.GetRequiredService<AppState>();

            MyBlazorWebView.Host = host;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Current counter value is {_appState.Counter}", "Counter Value");
        }
    }
}
