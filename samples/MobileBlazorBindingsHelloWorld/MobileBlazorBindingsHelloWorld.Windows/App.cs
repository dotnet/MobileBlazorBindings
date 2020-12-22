﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace MobileBlazorBindingsHelloWorld.Windows
{
    public class MainWindow : FormsApplicationPage
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new System.Windows.Application();
            app.Run(new MainWindow(args));
        }

        public MainWindow(string[] args)
        {
            Forms.Init();
            LoadApplication(new App(args));
        }
    }
}
