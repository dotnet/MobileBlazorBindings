// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    public class MobileBlazorBindingsRenderer : NativeComponentRenderer
    {
        public MobileBlazorBindingsRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
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
            var errorPage = new XF.ContentPage()
            {
                Title = "Unhandled exception",
                Content = new XF.StackLayout
                {
                    Padding = 10,
                    Children =
                    {
                        new XF.Label
                        {
                            FontAttributes = XF.FontAttributes.Bold,
                            FontSize = XF.Device.GetNamedSize(XF.NamedSize.Large, typeof(XF.Label)),
                            Text = "Unhandled exception",
                        },
                        new XF.Label
                        {
                            Text = exception?.Message,
                        },
                        new XF.ScrollView
                        {
                            Content =
                                new XF.Label
                                {
                                    FontSize = XF.Device.GetNamedSize(XF.NamedSize.Small, typeof(XF.Label)),
                                    Text = exception?.StackTrace,
                                },

                        },
                    },
                },
            };

            return errorPage;
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new MobileBlazorBindingsElementManager();
        }
    }
}
