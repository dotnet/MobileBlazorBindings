using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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



            var hostBuilder = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Adds web-specific services such as NavigationManager
                    services.AddBlazorHybrid();

                    // Register app-specific services
                    //services.AddSingleton<AppState>();
                })
                .UseWebRoot("wwwroot");

            //if (fileProvider != null)
            //{
            //    hostBuilder.UseStaticFiles(fileProvider);
            //}
            //else
            {
                hostBuilder.UseStaticFiles();
            }

            MyBlazorWebView.Host = hostBuilder.Build();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyBlazorWebView.WebView.Source = new Uri("https://google.com");
        }
    }


    public static class MobileBlazorBindingsHost
    {
        public static IHostBuilder CreateDefaultBuilder(string[] args = null)
        {
            // Inspired by Microsoft.Extensions.Hosting.Host, which can be seen here:
            // https://github.com/dotnet/runtime/blob/master/src/libraries/Microsoft.Extensions.Hosting/src/Host.cs
            // But slightly modified to work on all of Android, iOS, and UWP.

            var builder = new HostBuilder();

            builder.UseContentRoot(Directory.GetCurrentDirectory());
            builder.UseWebRoot("wwwroot");

            builder.UseDefaultServiceProvider((context, options) =>
                {
                    var isDevelopment = context.HostingEnvironment.IsDevelopment();
                    options.ValidateScopes = isDevelopment;
                    options.ValidateOnBuild = isDevelopment;
                });

            builder.ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddSingleton<BlazorHybridRenderer>();
            });

            return builder;
        }
    }

    public static class BlazorHybridHostExtensions
    {
        /// <summary>
        /// Specify the webroot directory to be used by the hybrid host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <param name="webRoot">Path to the root directory used by the hybrid host.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder UseWebRoot(this IHostBuilder hostBuilder, string webRoot)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (string.IsNullOrEmpty(webRoot))
            {
                throw new ArgumentException($"'{nameof(webRoot)}' cannot be null or empty", nameof(webRoot));
            }

            return hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                configurationBuilder.Properties[BlazorHybridDefaults.WebRootKey] = webRoot;
            });
        }

        public static IHostBuilder UseStaticFiles(this IHostBuilder hostBuilder)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            IFileProvider rootProvider = null;

            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                if (!configurationBuilder.Properties.TryGetValue(BlazorHybridDefaults.WebRootKey, out object webRoot))
                {
                    throw new KeyNotFoundException("WebRoot not set.");
                }

                rootProvider = new PhysicalFileProvider(
                    System.IO.Path.Combine(hostBuilderContext.HostingEnvironment.ContentRootPath, (string)webRoot),
                    Microsoft.Extensions.FileProviders.Physical.ExclusionFilters.Sensitive);

                configurationBuilder.Properties[BlazorHybridDefaults.WebRootFileProvider] = rootProvider;
            });

            return hostBuilder.ConfigureServices(serviceCollection =>
            {
                serviceCollection.AddSingleton<IFileProvider>(rootProvider);
            });
        }
    }
}
