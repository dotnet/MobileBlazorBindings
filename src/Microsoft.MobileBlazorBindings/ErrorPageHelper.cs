// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Diagnostics;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    public static class ErrorPageHelper
    {
        public static void ShowExceptionPage(Exception exception)
        {
            Debug.WriteLine($"Fatal exception caught: '{exception?.GetType().Name}': '{exception?.Message}'");

            XF.Device.InvokeOnMainThreadAsync(() =>
            {
                XF.Application.Current.MainPage = GetErrorPageForException(exception);
            });
        }

        private static XF.ContentPage GetErrorPageForException(Exception exception)
        {
            var errorPage = new XF.ContentPage()
            {
                BackgroundColor = XF.Color.White,
                Title = "Unhandled exception",
                Content = new XF.StackLayout
                {
                    Margin = new XF.Thickness(0, top: 30, 0, 0),
                    Padding = 10,
                    Children =
                    {
                        new XF.Label
                        {
                            TextColor = XF.Color.Black,
                            FontAttributes = XF.FontAttributes.Bold,
                            FontSize = XF.Device.GetNamedSize(XF.NamedSize.Large, typeof(XF.Label)),
                            Text = "Unhandled exception",
                        },
                        new XF.Label
                        {
                            TextColor = XF.Color.Black,
                            Text = exception?.Message,
                        },
                        new XF.ScrollView
                        {
                            Content =
                                new XF.Label
                                {
                                    TextColor = XF.Color.Black,
                                    FontSize = XF.Device.GetNamedSize(XF.NamedSize.Small, typeof(XF.Label)),
                                    Text = exception?.StackTrace,
                                },

                        },
                    },
                },
            };

            return errorPage;
        }
    }
}
