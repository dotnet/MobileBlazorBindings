// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings;

namespace HybridApp
{
    public class App : Application
    {
        public App(MobileBlazorBindingsRenderer renderer)
        {
            MainPage = new ContentPage();
            _ = renderer.AddComponent<MainPage>(parent: MobileBlazorBindingsHostExtensions.CreateHandler(this, renderer));
        }
    }
}
