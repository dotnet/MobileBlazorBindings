using Microsoft.MobileBlazorBindings;

namespace ControlGallery
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services
                .AddSingleton<ShellNavigationManager>()
                .AddScoped<MobileBlazorBindingsRenderer>();

            return builder.Build();
        }
    }
}