using MobileClient.UI.Pages;
using MobileClient.UI.Pages.Handheld;

namespace MobileClient.UI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        BindingContext = this;
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

    private string _selectedRoute;
    public string SelectedRoute
    {
        get => _selectedRoute;
        set
        {
            _selectedRoute = value;
            OnPropertyChanged();
        }
    }

    private async void OnMenuItemChangedAsync(object sender, CheckedChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(_selectedRoute))
            await Current.GoToAsync($"//{_selectedRoute}");
    }
}