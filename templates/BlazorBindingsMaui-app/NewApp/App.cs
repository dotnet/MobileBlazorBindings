using BlazorBindings.Maui;

namespace NewApp;

public partial class App : Application
{
    public App(MauiBlazorBindingsRenderer renderer)
    {
        renderer.AddComponent<AppShell>(this);
    }
}
