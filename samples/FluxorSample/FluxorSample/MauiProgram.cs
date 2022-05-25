using BlazorBindings.Maui;
using Fluxor;

namespace FluxorSample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiBlazorBindings()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services
                .AddHttpClient()
                .AddFluxor(options => options.ScanAssemblies(typeof(MauiProgram).Assembly));

            return builder.Build();
        }
    }
}