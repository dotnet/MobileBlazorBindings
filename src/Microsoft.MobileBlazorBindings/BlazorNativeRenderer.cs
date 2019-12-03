using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native
{
    public class BlazorNativeRenderer : EmblazonRenderer
    {
        public BlazorNativeRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        public override Dispatcher Dispatcher { get; } = new XamarinDeviceDispatcher();

        protected override void HandleException(Exception exception)
        {
            Debug.WriteLine($"{nameof(HandleException)} called with '{exception?.GetType().Name}': '{exception?.Message}'");

            XF.Device.InvokeOnMainThreadAsync(() =>
            {
                XF.Application.Current.MainPage = GetErrorPageForException(exception);
            });
        }

        private static XF.ContentPage GetErrorPageForException(Exception exception)
        {
            var stackLayout = new XF.StackLayout
            {
                Padding = 10,
            };
            stackLayout.Children.Add(
                new XF.Label
                {
                    FontAttributes = XF.FontAttributes.Bold,
                    FontSize = XF.Device.GetNamedSize(XF.NamedSize.Large, typeof(XF.Label)),
                    Text = "Unhandled exception",
                });
            stackLayout.Children.Add(
                new XF.Label
                {
                    Text = exception?.Message,
                });
            stackLayout.Children.Add(
                new XF.ScrollView
                {
                    Content =
                        new XF.Label
                        {
                            FontSize = XF.Device.GetNamedSize(XF.NamedSize.Small, typeof(XF.Label)),
                            Text = exception?.StackTrace,
                        },

                });

            var errorPage = new XF.ContentPage()
            {
                Title = "Unhandled exception",
                Content = stackLayout,
            };
            return errorPage;
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new BlazorNativeElementManager();
        }
    }
}
