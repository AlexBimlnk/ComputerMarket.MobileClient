namespace MobileClient.UI.Pages.Views;

public class ManagerProfileView : ContentView
{
    public ManagerProfileView()
    {
        var links = new Button
        {
            WidthRequest = 200,
            Text = "Links"
        };
        links.Clicked += async (o, e) => await Shell.Current.GoToAsync(nameof(LinkPage), true, new Dictionary<string, object>());

        Content = new VerticalStackLayout
        {
            Children = {
                links
            }
        };
    }
}