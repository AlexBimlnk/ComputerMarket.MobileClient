using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages;

public class CatalogProductPageView : ContentPage
{
	public CatalogProductPageView(CatalogProductViewModel model)
	{
        BindingContext = model;

        var image = new Image
        {
            Aspect = Aspect.AspectFill,
            HeightRequest = 200,
            WidthRequest = 200
        }.Bind(Image.SourceProperty, "Product.Item.URL");

        var title = new Label
        {
            FontAttributes = FontAttributes.Bold,
            FontSize = 20
        }.Bind(Label.TextProperty, "Product.Item.Name");

        var provider = new Label
        {
            VerticalOptions = LayoutOptions.End
        }.Bind(Label.TextProperty, "Product.Provider.Name");

        var button = new Button
        {
            Text = "Добавить",
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Start
        };

        button.Clicked += async (s, e) => await (BindingContext as CatalogProductViewModel).AddToBasketAsync();

        var state = new Label
        {
            FontSize = 15
        }.Bind(Label.TextProperty, "BasketState");

        Content = new ScrollView
		{
			Content = new VerticalStackLayout
            {
                Padding=20,
                Spacing = 10,
                VerticalOptions = LayoutOptions.Start,
                Children = {image, title, provider, state, button}
            }
		};
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as CatalogProductViewModel).UpdateBasketStateAsync();
}