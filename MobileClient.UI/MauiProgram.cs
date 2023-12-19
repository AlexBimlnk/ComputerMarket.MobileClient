using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MobileClient.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        //var a = Assembly.GetExecutingAssembly();
       // using var stream = a.GetManifestResourceStream("MauiApp27.");

        var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();


        builder.Configuration.AddConfiguration(config);

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Services.AddMyServices(builder.Configuration)
            .AddSingleton<MainPage>();

        return builder.Build();
    }
}