using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace WpfNewBlazorSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IServiceProvider Services { get; }

        public MainWindow()
        {
            var serviceCollection = new ServiceCollection();
            Services = serviceCollection.BuildServiceProvider();
            Resources.Add("services", Services);

            InitializeComponent();
        }
    }
}
