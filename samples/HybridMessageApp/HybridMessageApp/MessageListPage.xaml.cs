// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using HybridMessageApp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HybridMessageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageListPage : ContentPage
    {
        private readonly ContentPage detailsPage = new ContentPage
        {
            Content = new BlazorWebView<WebUI.MessageDetails> { Host = MainPage.Host }
        };
        private readonly AppState appState;

        public MessageListPage()
        {
            InitializeComponent();

            MessageListWebView.Host = MainPage.Host;

            appState = MainPage.Host.Services.GetRequiredService<AppState>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            appState.CurrentMessageChanged += NavigateToMessage;

            // Workaround for "master" icon not appearing if you:
            //  1. Use portrait mode on tablet
            //  2. Navigate into a subpage
            //  3. Rotate to landscape
            //  4. Click 'back'
            // This property assignment results in a call to UpdateTitleArea on NavigationRenderer
            var flyout = ((FlyoutPage)Parent.Parent).Flyout;
            var originalValue = flyout.IconImageSource;
            flyout.IconImageSource = "temp.png";
            flyout.IconImageSource = originalValue;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            appState.CurrentMessageChanged -= NavigateToMessage;
        }

        private void NavigateToMessage(object sender, Message e)
        {
            detailsPage.Title = e.Subject;
            _ = Navigation.PushAsync(detailsPage);
        }
    }
}
