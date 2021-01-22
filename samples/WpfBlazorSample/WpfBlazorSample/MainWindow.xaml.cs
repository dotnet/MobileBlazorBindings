using Microsoft.AspNetCore.Components;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class WpfBlazorWebView<TComponent> : Control
        where TComponent : IComponent
    {
        private WebView2 _webView;

        public WpfBlazorWebView()
        {
            var template = new ControlTemplate
            {
                VisualTree = new FrameworkElementFactory(typeof(WebView2), "WebView2")
            };

            Template = template;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var x = GetTemplateChild("WebView2");
            _webView = (WebView2)x;
            _webView.Source = new Uri("https://bing.com/");
            _webView.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;
        }

        private void CoreWebView2_WebResourceRequested(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequestedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
