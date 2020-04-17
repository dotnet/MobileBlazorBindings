using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessageApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new MyRootMasterDetailPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
