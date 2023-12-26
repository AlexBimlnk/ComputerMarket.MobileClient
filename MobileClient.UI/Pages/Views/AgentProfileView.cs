using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Helpers;
using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages.Views;

public class AgentProfileView : ContentView
{
	public AgentProfileView()
	{
        BindingContext = ServiceHelper.GetService<UserProfileViewModel>();

        var greeting = new Label
        {
            FontSize = 34,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        }.Bind(Label.TextProperty, "Greeting");

        Content = new VerticalStackLayout
        {
            Children = {
                greeting
            }
        };
    }
}