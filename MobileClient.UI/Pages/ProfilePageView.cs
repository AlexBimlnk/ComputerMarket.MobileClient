using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;
using MobileClient.UI.Pages.Views;

namespace MobileClient.UI.Pages;

public class ProfilePageView : ContentPage
{
    public ProfilePageView(ProfileViewModel model)
    {
        Title = "Профиль";
        BindingContext = model;
        Resources = new ResourceDictionary
        {
            {"CarouselDataTemplateSelector",  new CarouselDataTemplateSelector()}
        };

        var orders = new Button
        {
            WidthRequest = 300,
            Text = "Заказы",
            HorizontalOptions = LayoutOptions.Center
        };
        orders.Clicked += async (sender, e) => await Shell.Current.GoToAsync(nameof(OrdersPageView), true, new Dictionary<string, object>());

        var separator = new BoxView
        {
            Color = Colors.LightGray,
            HeightRequest = 1,
            Margin = 20
        };

        var greeting = new Label
        {
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            Margin = 30,
            HorizontalOptions = LayoutOptions.Start,
        }
            .Bind(Label.TextProperty, "Greetings");
        var view = new CarouselView
        {
            ItemTemplate = Resources["CarouselDataTemplateSelector"] as CarouselDataTemplateSelector,
            IsSwipeEnabled = false,
            IsScrollAnimated = false
        }.ItemsSource(new string[] { "1", "2", "3" }).Bind(
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