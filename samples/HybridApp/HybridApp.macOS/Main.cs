using AppKit;
using BlazorDesktop.macOS;

namespace MyApplication.macOS
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            BlazorDesktopMacOS.Init();
            NSApplication.Init();
            NSApplication.SharedApplication.Delegate = new AppDelegate();
            NSApplication.Main(args);
        }
    }
}
