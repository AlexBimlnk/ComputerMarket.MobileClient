using CommunityToolkit.Maui.Markup;

using MobileClient.Contract;
using MobileClient.UI.Pages.Models;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

[QueryProperty("Catalog", "Catalog")]
public class CatalogPageView : ContentPage
{
    public CatalogPageView(CatalogViewModel model)
    {
        BindingContext = model;
        Title = "Каталог";
        var button = new Button
        {
            Text = "Фильтр",
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Start,
            Margin = 30,
            FontSize = 15

        };
        button.Clicked += async (o, e) => await Shell.Current.GoToAsync(nameof(FilterPageView), true, new Dictionary<string, object>
        {
            ["Catalog"] = ((CatalogViewModel)BindingContext).Catalog
        });

        var view = new CollectionView
        {
            EmptyView = new Label
            {
                FontSize = 20,
                Text = "Каталог пустой"
            },
            SelectionMode = SelectionMode.Single
        }.ItemsSource(model.Products);

        view.SelectionChanged += async (o, e) =>
        {
            if (e.CurrentSelection.FirstOrDefault() is not Product item)
                return;
            await Shell.Current.GoToAsync(nameof(CatalogProductPageView), true, new Dictionary<string, object>
            {
                ["Product"] = item
            });
        };

        view.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var image = new Image
                {
                    Aspect = Aspect.AspectFill,
                    HeightRequest = 80,
                    WidthRequest = 80,
                    Margin = 10
                }.Bind(Image.SourceProperty, "Item.URL");
                var name = new Label
                {
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Item.Name");
                var provider = new Label
                {
                }.Bind(Label.TextProperty, "Provider.Name");
                var cost = new Label
                {

                }.Bind(Label.TextProperty, "FinalCost", stringFormat: "{0:0.00} ₸");
                var grid = new Grid
                {
                    Padding = 10,
                    RowDefinitions = Rows.Define(Auto, Auto),
                    ColumnDefinitions = Columns.Define(Auto, Auto, Auto),
                    Children =
                    {
                        image.RowSpan(2),
                        name.Column(1),
                        provider.Column(1).Row(1),
                        cost.Column(2).Row(1),
                    }
                };

                return grid;
            }
        };

        Content = new VerticalStackLayout
        {
            Children = { button, view }
        };
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as CatalogViewModel).ReloadDataAsync();
}