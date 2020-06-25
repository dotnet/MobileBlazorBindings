using AppKit;
using Microsoft.MobileBlazorBindings.WebView.macOS;

namespace HybridApp.macOS
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            BlazorHybridMacOS.Init();
            NSApplication.Init();
            NSApplication.SharedApplication.Delegate = new AppDelegate();
            NSApplication.Main(args);
        }
    }
}
