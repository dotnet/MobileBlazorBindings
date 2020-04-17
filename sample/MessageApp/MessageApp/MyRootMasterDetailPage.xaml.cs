using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessageApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRootMasterDetailPage : MasterDetailPage
    {
        public MyRootMasterDetailPage()
        {
            InitializeComponent();
            MasterBehavior = Device.Idiom == TargetIdiom.Tablet ? MasterBehavior.SplitOnLandscape : MasterBehavior.Default;
            NavigateToChild("Initial item");
        }

        private void NavigateToChild(string title)
        {
            DetailPage.Title = title;
            if (CanChangeIsPresented)
            {
                IsPresented = false;
            }
        }
    }
}
