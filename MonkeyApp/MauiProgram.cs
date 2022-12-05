using MonkeyApp.Model;
using MonkeyApp.Services;
using MonkeyApp.View;
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

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        builder.Services.AddSingleton<MonkeyService>();
        builder.Services.AddSingleton<MonkeysVM>();
        builder.Services.AddSingleton<MainPage>();

        //каждый раз создает новую страницу и новую вью модель для нее - каждуй обезьяне по странице
        builder.Services.AddTransient<MonkeyDetailsVM>();
        builder.Services.AddTransient<DetailsPage>();
        return builder.Build();
    }
}
