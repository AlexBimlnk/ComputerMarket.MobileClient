using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;
using MobileClient.UI.Pages.Views;

namespace MobileClient.UI.Pages;

public class ProfilePageView : ContentPage
{
	public ProfilePageView(ProfileViewModel model)
	{
        BindingContext = model;
        Resources = new ResourceDictionary
        {
            {"CarouselDataTemplateSelector",  new CarouselDataTemplateSelector()}
        };

        var greeting = new Label
        {
            FontSize = 20,
            FontAttributes = FontAttributes.Bold
        }
            .Bind(Label.TextProperty, "Greetings");
        var view = new CarouselView
        {
            ItemTemplate = Resources["CarouselDataTemplateSelector"] as CarouselDataTemplateSelector,
            IsSwipeEnabled = false,
            IsScrollAnimated = false
        }.ItemsSource(new string[] {"1", "2" }).Bind(
            CarouselView.PositionProperty,
            "PositionSelected");
		Content = new VerticalStackLayout
		{
			Children = {
				greeting, view
			}
		};
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as ProfileViewModel).UpdateUserAsync();
}