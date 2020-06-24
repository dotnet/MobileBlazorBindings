using BlazorDesktop.Windows;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace MyApplication.Windows
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
            BlazorDesktopWindows.Init();
            LoadApplication(new MyApplication.App());
        }
    }
}
