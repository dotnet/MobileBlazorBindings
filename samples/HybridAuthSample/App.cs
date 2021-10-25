// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using HybridAuthSample;
using Microsoft.Maui.Controls;
using Microsoft.MobileBlazorBindings;

namespace HybridAuthApp
{
    public class App : Application
    {
        public App(MobileBlazorBindingsRenderer renderer)
        {
            MainPage = new ContentPage();
            _ = renderer.AddComponent<Main>(parent: MobileBlazorBindingsHostExtensions.CreateHandler(this, renderer));
        }
    }
}
