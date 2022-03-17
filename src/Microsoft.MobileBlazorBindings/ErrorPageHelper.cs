// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Graphics;
using System;
using System.Diagnostics;

namespace Microsoft.MobileBlazorBindings
{
    public static class ErrorPageHelper
    {
        public static void ShowExceptionPage(Exception exception)
        {
            Debug.WriteLine($"Fatal exception caught: '{exception?.GetType().Name}': '{exception?.Message}'");

            Application.Current.Dispatcher.DispatchAsync(() =>
            {
                Application.Current.MainPage = GetErrorPageForException(exception);
            });
        }

        private static ContentPage GetErrorPageForException(Exception exception)
        {
            var errorPage = new ContentPage()
            {
                BackgroundColor = Colors.White,
                Title = "Unhandled exception",
                Content = new StackLayout
                {
                    Margin = new Thickness(0, top: 30, 0, 0),
                    Padding = 10,
                    Children =
                    {
                        new Label
                        {
                            TextColor = Colors.Black,
                            FontAttributes = FontAttributes.Bold,
                            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                            Text = "Unhandled exception",
                        },
                        new Label
                        {
                            TextColor = Colors.Black,
                            Text = exception?.Message,
                        },
                        new ScrollView
                        {
                            Content =
                                new Label
                                {
                                    TextColor = Colors.Black,
                                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
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
