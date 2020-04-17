using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace MessageApp.Windows
{
    public class MainWindow : FormsApplicationPage
    {
        [STAThread]
        public static void Main()
        {
            var app = new System.Windows.Application();
            app.Run(new MainWindow());
        }

        public MainWindow()
        {
            Forms.Init();
            LoadApplication(new MessageApp.App());
        }
    }
}
