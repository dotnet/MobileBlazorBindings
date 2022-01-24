// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Foundation;

namespace MobileBlazorBindingsToDo.Platforms.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}