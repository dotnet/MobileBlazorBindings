// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings;

namespace ControlGallery
{
    public class App : Application
    {
        public App(MobileBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<AppShell>(this);
        }
    }
}
