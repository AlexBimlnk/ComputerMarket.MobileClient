using CommunityToolkit.Maui.Markup;

using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.UI.Pages.Models;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

[QueryProperty("Catalog", "Catalog")]
public class CatalogPageView : ContentPage
{
	public CatalogPageView(CatalogViewModel model)
	{
        BindingContext = model;

        var button = new Button
        {
            Text = "Фильтр",
            WidthRequest = 100,
            HorizontalOptions = LayoutOptions.Start,

        };
        button.Clicked += async (o, e) => await Shell.Current.GoToAsync(nameof(FilterPageView), true, new Dictionary<string, object>
        {
            ["Catalog"] = ((CatalogViewModel)BindingContext).Catalog
        });

        var view = new CollectionView
        {
            SelectionMode = SelectionMode.Single,
            Header = new Label
            {
                Text = "Каталог",
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            }
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
                    HeightRequest = 60,
                    WidthRequest = 60
                }.Bind(Image.SourceProperty, "Item.URL");
                var name = new Label
                {
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Item.Name");
                var provider = new Label 
                {
                    VerticalOptions = LayoutOptions.End
                }.Bind(Label.TextProperty, "Provider.Name");

                var grid = new Grid
                {
                    Padding = 10,
                    RowDefinitions = Rows.Define(Auto, Auto),
                    ColumnDefinitions = Columns.Define(Auto, Auto),
                    Children =
                    {
                        image.RowSpan(3),
                        name.Column(1),
                        provider.Column(1)
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