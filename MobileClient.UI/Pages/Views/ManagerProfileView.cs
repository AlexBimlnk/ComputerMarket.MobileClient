namespace MobileClient.UI.Pages.Views;

public class ManagerProfileView : ContentView
{
    public ManagerProfileView()
    {
        var links = new Button
        {
            WidthRequest = 300,
            Margin = 30,
            Text = "Cвязи",
            HorizontalOptions = LayoutOptions.Center
        };
        var providers = new Button
        {
            WidthRequest = 300,
            Margin = 30,
            Text = "Поставщики",
            HorizontalOptions = LayoutOptions.Center
        };
        var orders = new Button
        {
            WidthRequest = 300,
            Margin = 30,
            Text = "Подтверждения",
            HorizontalOptions = LayoutOptions.Center
        };
        links.Clicked += async (o, e) => await Shell.Current.GoToAsync(nameof(LinksPageView), true, new Dictionary<string, object>());
        providers.Clicked += async (o, e) => await Shell.Current.GoToAsync(nameof(ProvidersPageView), true, new Dictionary<string, object>());
        orders.Clicked += async (o, e) => await Shell.Current.GoToAsync(nameof(ProcessableOrdersPageView), true, new Dictionary<string, object>());
        
        Content = new VerticalStackLayout
        {
            Children = {
                links, providers, orders
            }
        };
    }
}