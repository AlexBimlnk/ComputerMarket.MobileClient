using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

public class CatalogProductPageView : ContentPage
{
    public CatalogProductPageView(CatalogProductViewModel model)
    {

        /* Unmerged change from project 'MobileClient.UI (net8.0-maccatalyst)'
        Before:
                BindingContext = model;

                Title = "Товар";
        After:
                BindingContext = model;

                Title = "Товар";
        */

        /* Unmerged change from project 'MobileClient.UI (net8.0-ios)'
        Before:
                BindingContext = model;

                Title = "Товар";
        After:
                BindingContext = model;

                Title = "Товар";
        */

        /* Unmerged change from project 'MobileClient.UI (net8.0-windows10.0.19041.0)'
        Before:
                BindingContext = model;

                Title = "Товар";
        After:
                BindingContext = model;

                Title = "Товар";
        */
        BindingContext = model;

        Title = "Товар";
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
        var source = new Binding("Properties");
        var state = new Label
        {
            FontSize = 15
        }.Bind(Label.TextProperty, "BasketState");
        var separator = new BoxView
        {
            Color = Colors.LightGray,
            HeightRequest = 1,
            Margin = 20
        };
        var info = new CollectionView
        {
            Header = new Label
            {
                Text = "Характеристики",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold
            },
            Margin = 10
        }.ItemsSource(model.Properties);

        info.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var label = new Label
                {
                    Margin = 10,
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Name");
                var value = new Label
                {
                    Margin = 10
                }.Bind(Label.TextProperty, "Value"); ;

                var grid = new Grid
                {
                    Padding = 10,
                    RowDefinitions = Rows.Define(Auto),
                    ColumnDefinitions = Columns.Define(Auto, Auto),
                    Children =
                    {
                        label, value.Column(1)
                    }
                };

                return grid;
            }
        };

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 10,
                VerticalOptions = LayoutOptions.Start,
                Children = { image, title, provider, state, button, separator, info }
            }
        };
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as CatalogProductViewModel).UpdateBasketStateAsync();
}