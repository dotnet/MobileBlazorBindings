// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.IO;

namespace Microsoft.MobileBlazorBindings.Hosting
{
    public static class BlazorAssets
    {
        public static Stream GetBlazorDesktopJS()
        {
            return typeof(BlazorAssets).Assembly.GetManifestResourceStream("Microsoft.MobileBlazorBindings.Hosting.blazor.desktop.js");
        }
    }
}