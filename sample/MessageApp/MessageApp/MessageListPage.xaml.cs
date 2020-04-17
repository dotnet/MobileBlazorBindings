using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageListPage : ContentPage
    {
        readonly ContentPage subPage;

        public MessageListPage()
        {
            InitializeComponent();

            var subPageItems = new StackLayout();
            subPageItems.Children.Add(new Label { Text = "This was generated programmatically" });
            subPage = new ContentPage { Content = subPageItems };
        }

        private void OnMyNavigateClicked(object sender, EventArgs e)
        {
            subPage.Title = $"You clicked at {DateTime.Now.ToLongTimeString()}";
            _ = this.Navigation.PushAsync(subPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Workaround for "master" icon not appearing if you:
            //  1. Use portrait mode on tablet
            //  2. Navigate into a subpage
            //  3. Rotate to landscape
            //  4. Click 'back'
            // This property assignment results in a call to UpdateTitleArea on NavigationRenderer
            var master = ((MasterDetailPage)Parent.Parent).Master;
            var originalValue = master.IconImageSource;
            master.IconImageSource = "temp.png";
            master.IconImageSource = originalValue;
        }
    }
}
