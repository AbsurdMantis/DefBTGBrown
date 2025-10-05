using DefBTGBrown.ViewModels;
using DefBTGBrown.Views;
using Microsoft.Extensions.Logging;

namespace DefBTGBrown
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddSingleton<GraphicsViewPage>();

            return builder.Build();
        }
    }
}
