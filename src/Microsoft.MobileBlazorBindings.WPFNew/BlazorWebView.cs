using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Web.WebView2.Wpf;

namespace Microsoft.MobileBlazorBindings.WPFNew
{
    public sealed class BlazorWebView : Control, IDisposable
    {
        public static readonly DependencyProperty HostPageProperty = DependencyProperty.Register(
            name: nameof(HostPage),
            propertyType: typeof(string),
            ownerType: typeof(BlazorWebView),
            typeMetadata: new PropertyMetadata(OnHostPagePropertyChanged));

        public static readonly DependencyProperty RootComponentsProperty = DependencyProperty.Register(
            name: nameof(RootComponents),
            propertyType: typeof(ObservableCollection<RootComponent>),
            ownerType: typeof(BlazorWebView));

        public static readonly DependencyProperty ServicesProperty = DependencyProperty.Register(
            name: nameof(Services),
            propertyType: typeof(IServiceProvider),
            ownerType: typeof(BlazorWebView),
            typeMetadata: new PropertyMetadata(OnServicesPropertyChanged));


        private const string webViewTemplateChildName = "WebView";
        private WebView2 _webView2;
        private WebView2BlazorWebViewCore _core;

        public BlazorWebView()
        {
            SetValue(RootComponentsProperty, new ObservableCollection<RootComponent>());
            RootComponents.CollectionChanged += (_, ___) => StartWebViewCoreIfPossible();

            Template = new ControlTemplate
            {
                VisualTree = new FrameworkElementFactory(typeof(WebView2), webViewTemplateChildName)
            };
        }

        public string HostPage
        {
            get { return (string)GetValue(HostPageProperty); }
            set { SetValue(HostPageProperty, value); }
        }

        public ObservableCollection<RootComponent> RootComponents
            => (ObservableCollection<RootComponent>)GetValue(RootComponentsProperty);

        public IServiceProvider Services
        {
            get { return (IServiceProvider)GetValue(ServicesProperty); }
            set { SetValue(ServicesProperty, value); }
        }

        private static void OnServicesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((BlazorWebView)d).OnServicesPropertyChanged(e);

        private void OnServicesPropertyChanged(DependencyPropertyChangedEventArgs e) => StartWebViewCoreIfPossible();

        private static void OnHostPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((BlazorWebView)d).OnHostPagePropertyChanged(e);

        private void OnHostPagePropertyChanged(DependencyPropertyChangedEventArgs e) => StartWebViewCoreIfPossible();

        private bool RequiredStartupPropertiesSet =>
            _webView2 != null &&
            HostPage != null &&
            (RootComponents?.Any() ?? false) &&
            Services != null;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            StartWebViewCoreIfPossible();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _webView2 = (WebView2)GetTemplateChild(webViewTemplateChildName);

            StartWebViewCoreIfPossible();
        }

        private void StartWebViewCoreIfPossible()
        {
            if (!RequiredStartupPropertiesSet)
            {
                return;
            }

            // TODO: Can this get called multiple times, such as from OnApplyTemplate or a property change? Do we need to handle this more efficiently?
            _core?.Dispose();
            _core = new WebView2BlazorWebViewCore(_webView2, Services, WPFDispatcher.Instance, HostPage);

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
