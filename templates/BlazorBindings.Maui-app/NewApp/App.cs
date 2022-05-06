using Microsoft.MobileBlazorBindings;

namespace NewApp;

public partial class App : Application
{
    public App(MobileBlazorBindingsRenderer renderer)
    {
        renderer.AddComponent<MainPage>(this);
    }
}
