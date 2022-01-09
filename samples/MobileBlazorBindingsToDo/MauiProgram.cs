using Microsoft.MobileBlazorBindings;

namespace MobileBlazorBindingsToDo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMobileBlazorBindings();

            builder.Services.AddSingleton<AppState>();

            return builder.Build();
        }
    }
}