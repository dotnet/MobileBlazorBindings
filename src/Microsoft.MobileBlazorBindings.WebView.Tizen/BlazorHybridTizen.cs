// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Microsoft.MobileBlazorBindings.WebView.Tizen
{
    public static class BlazorHybridTizen
    {
        /// <summary>
        /// Ensures the initialization of required features for Blazor Hybrid applications.
        /// This method should be called as early as possible in the application startup logic.
        /// </summary>
        public static void Init()
        {
            // Calling this means the assembly will be loaded, so Xamarin.Forms will discover its ExportRenderer attributes
        }
    }
}
