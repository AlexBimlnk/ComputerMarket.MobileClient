using CommunityToolkit.Maui.Markup;

using MobileClient.Contract.Products;
using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages;

[QueryProperty("Catalog", "Catalog")]
public class FilterPageView : ContentPage
{
    public FilterPageView(FilterViewModel model)
    {
        BindingContext = model;
        var button = new Button
        {
            Text = "Применить",
            WidthRequest = 100,
            HorizontalOptions = LayoutOptions.Start
        };
        button.Clicked += async (o, e) => await Shell.Current.GoToAsync("///catalog", true, new Dictionary<string, object>
        {
            ["Catalog"] = (BindingContext as FilterViewModel).Catalog
        });

        Content = new VerticalStackLayout
        {
            Children = {
                new Entry
                {
                    Margin = 5
                }.Bind(Entry.TextProperty,
                    getter: static (FilterViewModel vm) => vm.Catalog.SearchString,
                    setter: static (FilterViewModel vm, string code) => vm.Catalog.SearchString = code
                ),
                button
            }
        };
    }
}