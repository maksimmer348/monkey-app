using MonkeyApp.Model;
using MonkeyApp.Services;
using MonkeyApp.VM;

namespace MonkeyApp;

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

        builder.Services.AddSingleton<MonkeyService>();
        builder.Services.AddSingleton<MonkeysVM>();
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
    }
}
