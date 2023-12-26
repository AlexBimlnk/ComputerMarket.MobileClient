using MobileClient.Logic.Account;
using MobileClient.UI.Helpers;
using MobileClient.UI.Pages;
using MobileClient.UI.Pages.Handheld;

namespace MobileClient.UI;

public partial class AppShellMobile : Shell
{
    private readonly ISignInManager _manager;
    public AppShellMobile()
    {
        _manager = ServiceHelper.GetService<ISignInManager>();
        InitializeComponent();

        InitRoutes();
    }

    private void InitRoutes()
    {
        Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));
        Routing.RegisterRoute(nameof(TipPage), typeof(TipPage));
        Routing.RegisterRoute(nameof(PayPage), typeof(PayPage));
        Routing.RegisterRoute(nameof(SignaturePage), typeof(SignaturePage));
        Routing.RegisterRoute(nameof(ReceiptPage), typeof(ReceiptPage));
        Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
        Routing.RegisterRoute(nameof(FilterPage), typeof(FilterPage));
    }

    public bool IsLogged
    {
        get => (bool)GetValue(IsLoggedProperty);
        set => SetValue(IsLoggedProperty, value);
    }

    public static readonly BindableProperty IsLoggedProperty =
        BindableProperty.Create(nameof(IsLogged), typeof(bool), typeof(AppShell), false, propertyChanged: IsLogged_PropertyChangedAsync);

    private async static void IsLogged_PropertyChangedAsync(BindableObject bindable, object oldValue, object newValue)
    {
        SetTabBarIsVisible(Application.Current.MainPage, false);

        if ((bool)newValue)
        {
            SetTabBarIsVisible(Application.Current.MainPage, true);
            await Shell.Current.GoToAsync("//home", true, new Dictionary<string, object>());
        }
        else
        {
            SetTabBarIsVisible(Application.Current.MainPage, false);
            await Shell.Current.GoToAsync("//home", true, new Dictionary<string, object>());
        }
    }

    public async Task CheckUserAsync()
    {
        var user = await _manager.GetCurrentUserAsync();
        if (user is null)
        {
            return;
        }
        
        (Shell.Current as AppShellMobile).IsLogged = true;
        await Shell.Current.GoToAsync("///home");
    }
}
