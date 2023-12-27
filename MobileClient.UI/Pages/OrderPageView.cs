using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

using
/* Unmerged change from project 'MobileClient.UI (net8.0-maccatalyst)'
Before:
using Org.Apache.Http.Cookies;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
After:
using Org.Apache.Http.Cookies;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;
*/

/* Unmerged change from project 'MobileClient.UI (net8.0-ios)'
Before:
using Org.Apache.Http.Cookies;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
After:
using Org.Apache.Http.Cookies;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;
*/

/* Unmerged change from project 'MobileClient.UI (net8.0-windows10.0.19041.0)'
Before:
using Org.Apache.Http.Cookies;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
After:
using Org.Apache.Http.Cookies;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;
*/
static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

[QueryProperty("Order", "Order")]
public class OrderPageView : ContentPage
{
    public OrderPageView(OrderViewModel model)
    {
        BindingContext = model;

        var cancel = new Button
        {
            Text = "Отмена",
            WidthRequest = 150,
            Margin = 20
        }.Bind(IsVisibleProperty, "CancelVisible");

        cancel.Clicked += async (o, e) =>
        {
            await (BindingContext as OrderViewModel).CancelAsync();
            await Navigation.PopAsync();
        };

        var pay = new Button
        {
            Text = "Оплата",
            WidthRequest = 150,
            Margin = 20
        }.Bind(IsVisibleProperty, "PayVisible");

        pay.Clicked += async (o, e) =>
        {
            await (BindingContext as OrderViewModel).PayAsync();
            await Navigation.PopAsync();
        };

        var state = new Label
        {
            FontSize = 30,
            FontAttributes = FontAttributes.Bold,
            Margin = 20
        }.Bind(Label.TextProperty, "Order.State");

        var label = new Label
        {
            FontSize = 24,
            Margin = 10,
            FontAttributes = FontAttributes.Bold
        }.Bind(Label.TextProperty, "Order.OrderDate", stringFormat: "Заказ от {0:d}");

        var layout = new Grid
        {
            Padding = 10,
            RowDefinitions = Rows.Define(Auto, Auto),
            ColumnDefinitions = Columns.Define(Auto, Auto),
            Children =
            {
                state.ColumnSpan(2),pay.Row(1),cancel.Column(1).Row(1)
            }
        };
        var separator = new BoxView
        {
            Color = Colors.LightGray,
            HeightRequest = 1,
            Margin = 20
        };
        var list = new CollectionView
        {
            Header = new Label
            {
                Text = "Товары",
                FontAttributes = FontAttributes.Bold
            }
        }.ItemsSource(model.Products);

        list.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var name = new Label
                {

                }.Bind(Label.TextProperty, "Product.Item.Name");
                var provider = new Label
                {

                }.Bind(Label.TextProperty, "Product.Provider.Name"); ;
                var quantity = new Label
                {

                }.Bind(Label.TextProperty, "Quantity");
                var cost = new Label
                {

                }.Bind(Label.TextProperty, "SumCost");

                return new VerticalStackLayout
                {
                    Children = { name, provider, quantity, cost }
                };

            }
        };

        Content = new VerticalStackLayout
        {
            Children = {
                label,
                layout, separator, list
            }
        };
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args) => (BindingContext as OrderViewModel).Update();
}