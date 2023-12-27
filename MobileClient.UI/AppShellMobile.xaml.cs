using MobileClient.Logic.Account;
using MobileClient.UI.Helpers;
using MobileClient.UI.Pages;

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
        Routing.RegisterRoute(nameof(CatalogProductPageView), typeof(CatalogProductPageView));
        Routing.RegisterRoute(nameof(FilterPageView), typeof(FilterPageView));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(LinkPage), typeof(LinkPage));
        Routing.RegisterRoute(nameof(OrderPageView), typeof(OrderPageView));
        Routing.RegisterRoute(nameof(OrdersPageView), typeof(OrdersPageView));
    }

    public bool IsLogged
    {
        get => (bool)GetValue(IsLoggedProperty);
        set => SetValue(IsLoggedProperty, value);
    }

    public static readonly BindableProperty IsLoggedProperty =
        BindableProperty.Create(nameof(IsLogged), typeof(bool), typeof(AppShellMobile), false, propertyChanged: IsLogged_PropertyChangedAsync);

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
