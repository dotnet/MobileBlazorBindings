// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

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
