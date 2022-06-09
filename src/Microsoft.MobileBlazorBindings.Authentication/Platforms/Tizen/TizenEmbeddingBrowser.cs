// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Threading;
using Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Authentication.Services.Tizen
{

    /// <summary>
    /// An implementation embedding browser for tizen platform
    /// </summary>
    sealed class TizenEmbeddingBrowser : ContentPage, IDisposable
    {
        INavigation _navigation;
        WebView _webView;
        CancellationTokenSource _cts;
        Label _title;

        /// <summary>
        /// Initializes a new instance.
        /// <param name="cts">A CancellationTokenSource to cancel authentication process</param>
        /// </summary>
        public TizenEmbeddingBrowser(CancellationTokenSource cts)
        {
            _cts = cts;
            _title = new Label
            {
                Text = "OAuth2 Authentication",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalTextAlignment = TextAlignment.Center,
            };

            _webView = new WebView();

            var frame = new Frame
            {
                BorderColor = Color.Transparent,
                Content = _title,
                Padding = 10,
                HasShadow = true,
            };
            Grid.SetRow(frame, 0);
            Grid.SetRow(_webView, 1);


            Content = new Grid
            {
                RowSpacing = 0,
                BackgroundColor = Color.FromHex("#eeeeee"),
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star },
                },
                Children =
                {
                    frame,
                    _webView,
                }
            };
        }

        /// <summary>
        /// Start navigate to authentication page
        /// </summary>
        /// <param name="url">The start URL</param>
        public void StartNavigate(Uri url)
        {
            _title.Text = url.Host;
            _webView.Source = url;

            _navigation = Application.Current.MainPage.Navigation;
            _navigation.PushModalAsync(this);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                CloseSelf();
                return false;
            });
        }

        /// <inheritdoc />
        protected override bool OnBackButtonPressed()
        {
            _cts?.CancelAfter(1000);
            _cts = null;
            return base.OnBackButtonPressed();
        }

        private void CloseSelf()
        {
            if (_navigation != null && _navigation.ModalStack.Count > 0 && _navigation.ModalStack[_navigation.ModalStack.Count - 1] == this)
                _navigation.PopModalAsync();
        }
    }
}
