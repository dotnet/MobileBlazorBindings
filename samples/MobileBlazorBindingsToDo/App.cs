// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings;

namespace MobileBlazorBindingsToDo
{
    public partial class App : Application
    {
        public App(MobileBlazorBindingsRenderer renderer)
        {
            MainPage = new TabbedPage();
            renderer.AddComponent<TodoApp>(MainPage);
        }
    }
}