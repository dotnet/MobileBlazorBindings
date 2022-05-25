using BlazorBindings.Maui;

namespace FluxorSample
{
    public partial class App : Application
    {
        public App(MauiBlazorBindingsRenderer renderer)
        {
            renderer.AddComponent<AppShell>(this);
        }
    }
}