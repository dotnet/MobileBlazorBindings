// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Foundation;
using Microsoft.Identity.Client;
using UIKit;
using Xamarin.Forms;

namespace HybridMsalAuthApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // For iOS, wrap inside a navigation page, otherwise the header looks wrong
            var formsApp = new App();
            formsApp.MainPage = new NavigationPage(formsApp.MainPage);

            LoadApplication(formsApp);

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            if (AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url))
            {
                return true;
            }

            return base.OpenUrl(app, url, options);
        }
    }
}
