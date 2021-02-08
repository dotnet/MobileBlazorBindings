using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings;
using Microsoft.MobileBlazorBindings.Hosting;
using RazorClassLibrarySample;
using System;
using System.Windows.Forms;

namespace WinFormsBlazorSample
{
    public partial class Form1 : Form
    {
        private readonly AppState _appState;

        public Form1()
        {
            InitializeComponent();

            blazorWebView1.ComponentType = typeof(RazorClassLibrarySample.SampleWebComponent);

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

            blazorWebView1.Host = host;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Current counter value is {_appState.Counter}", "Counter Value");
        }
    }
}
