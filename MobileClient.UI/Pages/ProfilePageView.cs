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

        var orders = new Button
        {
            WidthRequest = 200,
            Text = "Заказы"
        };
        orders.Clicked += async (sender, e) => await Shell.Current.GoToAsync(nameof(OrdersPageView), true, new Dictionary<string, object>());

        var separator = new BoxView
        {
            Color = Colors.DarkBlue,
            HeightRequest = 1
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
        }.ItemsSource(new string[] {"1", "2", "3" }).Bind(
            CarouselView.PositionProperty,
            "PositionSelected");
		Content = new VerticalStackLayout
		{
			Children = {
				greeting, orders, separator, view
            }
		};
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as ProfileViewModel).UpdateUserAsync();
}