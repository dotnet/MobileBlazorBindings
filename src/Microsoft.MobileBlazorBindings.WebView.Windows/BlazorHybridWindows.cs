// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.IO;
using System.Reflection;

namespace Microsoft.MobileBlazorBindings.WebView.Windows
{
    public static class BlazorHybridWindows
    {
        /// <summary>
        /// Allows you to override where the WebView2 user data is stored
        /// </summary>
        public static string WebViewDirectory { get; private set; }
        
        /// <summary>
        /// Ensures the initialization of required features for Blazor Hybrid applications.
        /// This method should be called as early as possible in the application startup logic.
        /// </summary>
        public static void Init(string webviewDirectory = null)
        {
            if (webviewDirectory == null)
            {
                var applicationName = (Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).GetName().Name;
                webviewDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationName);
            }
            WebViewDirectory = webviewDirectory;
            // Calling this means the assembly will be loaded, so Xamarin.Forms will discover its ExportRenderer attributes
        }
    }
}
