using Microsoft.Extensions.DependencyInjection;
using RazorClassLibrarySample;
using System.Windows;

namespace WpfNewBlazorSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBlazorWebViews();
            serviceCollection.AddSingleton<AppState>();

            Resources.Add("services", serviceCollection.BuildServiceProvider());

            InitializeComponent();
        }
    }
}
