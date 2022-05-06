// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui;

namespace ControlGallery
{
    public class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<AppShell>(this);
        }
    }
}
