using MobileClient.UI.Pages;

namespace MobileClient.UI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        BindingContext = this;
        InitRoutes();
    }

    private void InitRoutes() => Routing.RegisterRoute(nameof(AddProductView), typeof(AddProductView));

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