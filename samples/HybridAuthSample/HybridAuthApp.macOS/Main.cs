// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using AppKit;
using Microsoft.MobileBlazorBindings.WebView.macOS;

namespace HybridAuthApp.macOS
{
    internal static class MainClass
    {
        private static void Main(string[] args)
        {
            BlazorHybridMacOS.Init();
            NSApplication.Init();
            NSApplication.SharedApplication.Delegate = new AppDelegate();
            NSApplication.Main(args);
        }
    }
}
