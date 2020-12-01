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
        /// The location where the WebView2 user data is stored. If not set, an automatic location will be calculated
        /// based on the entry assembly's name (if available) or the calling assembly's name when the <see cref="Init(string)"/>
        /// method is called. The automatic location will be within the <see cref="Environment.SpecialFolder.LocalApplicationData"/>
        /// folder.
        /// </summary>
        public static string WebViewDirectory { get; private set; }
        
        /// <summary>
        /// Ensures the initialization of required features for Blazor Hybrid applications.
        /// This method should be called as early as possible in the application startup logic.
        /// </summary>
        /// <param name="webViewDirectory">The location where WebView2 user data is stored. See the <see cref="WebViewDirectory"/>
        /// property for more information.</param>
        public static void Init(string webViewDirectory = null)
        {
            if (webViewDirectory == null)
            {
                var applicationName = (Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).GetName().Name;
                webViewDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationName);
            }
            WebViewDirectory = webViewDirectory;

            // Calling this means the assembly will be loaded, so Xamarin.Forms will discover its ExportRenderer attributes
        }
    }
}
