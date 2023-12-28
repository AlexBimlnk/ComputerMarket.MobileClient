namespace MobileClient.UI.Pages.Views;

public class AgentProfileView : ContentView
{
    public AgentProfileView()
    {
        var orders = new Button
        {
            WidthRequest = 300,
            Margin = 30,
            Text = "Подтверждения",
            HorizontalOptions = LayoutOptions.Center
        };
        
        orders.Clicked += async (o, e) => await Shell.Current.GoToAsync(nameof(ProcessableOrdersPageView), true, new Dictionary<string, object>());

        Content = new VerticalStackLayout
        {
            Children = {
                orders
            }
        };
    }
}