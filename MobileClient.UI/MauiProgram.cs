using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

using Microsoft.Extensions.Configuration;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;

using MobileClient.UI;
using MobileClient.UI.Pages;

using MobileClient.UI.Pages;
using MobileClient.UI.Pages.Views;

using SkiaSharp.Views.Maui.Controls.Hosting;


#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;

using Windows.Graphics;
#endif

#if ANDROID
[assembly: Android.App.UsesPermission(Android.Manifest.Permission.Camera)]
#endif

namespace MobileClient.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var config = new ConfigurationBuilder()
            .Build();

        builder.Configuration.AddConfiguration(config);

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("opensans_semibold.ttf", "OpenSansSemiBold");
                fonts.AddFont("fa_solid.ttf", "FontAwesome");
                fonts.AddFont("fabmdl2.ttf", "Fabric");
            })
            .Services.AddMyServices(builder.Configuration)
            .AddSingleton<CatalogPage>()
            .AddSingleton<ProfilePage>()
            .AddSingleton<FilterPage>()
            .AddSingleton<LoginPage>()
            .AddSingleton<BasketPage>()
            .AddSingleton<ProductPage>()
            .AddSingleton<HomePage>();


#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    var nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                    var winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

                    const int width = 1200;
                    const int height = 800;
                    var x = 1920 / 2 - width / 2; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width)
                    var y = 1080 / 2 - height / 2; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height)

                    winuiAppWindow.MoveAndResize(new RectInt32(x, y, width, height));
                });
            });
        });
#endif

        ModifyEntry();

        return builder.Build();
    }

    public static void ModifyEntry()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoMoreBorders", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif IOS || MACCATALYST
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
            handler.PlatformView.FontWeight = Microsoft.UI.Text.FontWeights.Thin;
#endif
        });
    }
}
