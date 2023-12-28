using CommunityToolkit.Maui.Markup;

using MobileClient.Contract.Orders;
using MobileClient.UI.Pages.Models;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

public class ProcessableOrdersPageView : ContentPage
{
	public ProcessableOrdersPageView(ProcessableOrdersViewModel model)
	{
        Title = "Заказы на обработку";
        BindingContext = model;
        var view = new CollectionView
        {
            EmptyView = new Label { Text = "Нет активных заказов", FontSize = 20 },
            SelectionMode = SelectionMode.Single,
        }
        .ItemsSource(model.Items);

        view.SelectionChanged += async (o, e) =>
        {
            if (e.CurrentSelection.FirstOrDefault() is not Order item)
                return;
            await Shell.Current.GoToAsync(nameof(ProcessableOrderPageView), true, new Dictionary<string, object>
            {
                ["Order"] = item
            });
        };

        view.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var key = new Label
                {
                    Margin = 10,
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Key.Value");
                var state = new Label
                {
                    Margin = 10,
                    VerticalOptions = LayoutOptions.End
                }.Bind(Label.TextProperty, "State");
                var date = new Label
                {
                    Margin = 10,
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "OrderDate", stringFormat: "{0:d}");
                var sum = new Label
                {
                    Margin = 10,
                    VerticalOptions = LayoutOptions.End
                }.Bind(Label.TextProperty, "Sum", stringFormat: "{0:0.00} ₸");
                var separator = new BoxView
                {
                    Color = Colors.LightGray,
                    HeightRequest = 1,
                    Margin = 20
                };
                var layout = new Grid
                {
                    Padding = 10,
                    RowDefinitions = Rows.Define(Auto, Auto, Auto),
                    ColumnDefinitions = Columns.Define(30, 100, 300),
                    Children =
                    {
                        key.RowSpan(2),
                        date.Column(1).RowSpan(2),
                        state.Column(3).Row(1),
                        sum.Column(3),
                        separator.Row(2).ColumnSpan(3)
                    }
                };

                return layout;
            }
        };

        Content = new ScrollView
        {
            Content = view
        };
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as ProcessableOrdersViewModel).ReloadDataAsync();
}