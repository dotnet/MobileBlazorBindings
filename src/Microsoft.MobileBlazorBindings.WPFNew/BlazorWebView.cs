using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Web.WebView2.Wpf;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    public sealed class BlazorWebView : Control, IDisposable
    {
        private const string webViewTemplateChildName = "WebView";
        private WebView2BlazorWebViewCore _core;

        public BlazorWebView()
        {
            SetValue(RootComponentsProperty, new ObservableCollection<RootComponent>());

            Template = new ControlTemplate
            {
                VisualTree = new FrameworkElementFactory(typeof(WebView2), webViewTemplateChildName)
            };
        }

        public string HostPage { get; set; }

        public static readonly DependencyProperty RootComponentsProperty = DependencyProperty.Register(
            nameof(RootComponents),
            typeof(ObservableCollection<RootComponent>),
            typeof(BlazorWebView));

        public ObservableCollection<RootComponent> RootComponents
            => (ObservableCollection<RootComponent>)GetValue(RootComponentsProperty);

        public static readonly DependencyProperty ServicesProperty = DependencyProperty.Register(
            nameof(Services),
            typeof(IServiceProvider),
            typeof(BlazorWebView));

        public IServiceProvider Services
        {
            get { return (IServiceProvider)GetValue(ServicesProperty); }
            set { SetValue(ServicesProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var webview = (WebView2)GetTemplateChild(webViewTemplateChildName);

            // TODO: Can OnApplyTemplate get called multiple times? Do we need to handle this more efficiently?
            _core?.Dispose();
            _core = new WebView2BlazorWebViewCore(webview, Services, WPFDispatcher.Instance, HostPage);

            // TODO: Consider respecting the observability of this collection
            var addRootComponentTasks = RootComponents.Select(
                rootComponent => _core.AddRootComponentAsync(rootComponent.Type, rootComponent.Selector, ParameterView.Empty));

            Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    await Task.WhenAll(addRootComponentTasks).ConfigureAwait(true);
                    await _core.StartAsync().ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    // TODO: Figure out how to ensure any exceptions propagate to a central handler
                    // without needing this try/catch. Why doesn't it propagate to the unhandled exception
                    // event already?
                    MessageBox.Show(ex.ToString());
                    throw;
                }
            });
        }

        public void Dispose()
        {
            // TODO: Determine correct disposal pattern for WPF
            // How do we DisposeAsync properly?
            _core?.Dispose();
        }
    }
}
