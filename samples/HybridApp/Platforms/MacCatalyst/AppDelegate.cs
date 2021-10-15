using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace HybridApp
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}