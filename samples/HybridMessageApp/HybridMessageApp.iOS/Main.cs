using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MobileBlazorBindings.WebView.iOS;
using Foundation;
using UIKit;

namespace HybridMessageApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            BlazorHybridIOS.Init();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
