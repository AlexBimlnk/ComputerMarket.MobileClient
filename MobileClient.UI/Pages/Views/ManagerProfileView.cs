using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Helpers;
using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages.Views;

public class ManagerProfileView : ContentView
{
	public ManagerProfileView()
	{
        BindingContext = ServiceHelper.GetService<UserProfileViewModel>();

        var greeting = new Label
        {
            FontSize = 34,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        }.Bind(Label.TextProperty, "Greeting");

        var links = new Button
        {
            WidthRequest = 200,
            Text = "Links"
        };
        links.Clicked += async (o, e) => await Shell.Current.GoToAsync(nameof(LinkPage), true, new Dictionary<string, object>());

        Content = new VerticalStackLayout
		{
			Children = {
				greeting, links
			}
		};
	}
}