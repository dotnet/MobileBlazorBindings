// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using BlazorBindings.Maui;

namespace BlazorBindingsToDo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiBlazorBindings();

            builder.Services.AddSingleton<AppState>();

            return builder.Build();
        }
    }
}