using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

[QueryProperty("Catalog", "Catalog")]
public class FilterPageView : ContentPage
{
    public FilterPageView(FilterViewModel model)
    {
        Title = "Фильтр";
        BindingContext = model;
        var button = new Button
        {
            Text = "Применить",
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Start,
            Margin = 20
        };
        button.Clicked += async (o, e) => await Shell.Current.GoToAsync("///catalog", true, new Dictionary<string, object>
        {
            ["Catalog"] = (BindingContext as FilterViewModel).Catalog
        });

        Content = new VerticalStackLayout
        {
            Children = {
                button,
                new Grid
                {
                    Padding = 10,
                    RowDefinitions = Rows.Define(Auto),
                    ColumnDefinitions = Columns.Define(Auto, Auto),
                    Children =
                    {
                        new Label
                        {
                            Text = "Поиск",
                            FontSize = 20,
                            Margin = 10,
                            FontAttributes = FontAttributes.Bold
                        },
                        new Entry
                        {
                            FontSize = 20
                        }.Bind(Entry.TextProperty,
                            getter: static (FilterViewModel vm) => vm.Catalog.SearchString,
                            setter: static (FilterViewModel vm, string code) => vm.Catalog.SearchString = code
                        ).Column(1)
                    }
                }

            }
        };
    }
}