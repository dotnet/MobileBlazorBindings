// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui;

namespace BlazorBindingsToDo
{
    public partial class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            MainPage = new TabbedPage();
            renderer.AddComponent<TodoApp>(MainPage);
        }
    }
}