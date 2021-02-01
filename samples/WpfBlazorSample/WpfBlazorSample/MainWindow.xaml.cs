using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings.Hosting;
using System;
using System.Windows;

namespace WpfBlazorSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
                    //services.AddSingleton<AppState>();
                })
                .UseWebRoot("wwwroot");

            hostBuilder.UseStaticFiles();

            MyBlazorWebView.Host = hostBuilder.Build();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyBlazorWebView.WebView.Source = new Uri("https://google.com");
        }
    }
}
