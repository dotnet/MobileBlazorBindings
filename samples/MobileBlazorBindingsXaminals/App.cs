// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings;

namespace MobileBlazorBindingsXaminals
{
    public class App : Application
    {
        public App(MobileBlazorBindingsRenderer renderer)
        {
            MainPage = new ContentPage();
            _ = renderer.AddComponent<AppShell>(parent: MobileBlazorBindingsHostExtensions.CreateHandler(this, renderer));
        }
    }
}
