using CommunityToolkit.Maui.Markup;

using MobileClient.Contract;
using MobileClient.UI.Pages.Models;
using Org.Apache.Http.Authentication;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

[QueryProperty("Order", "Order")]
public class ProcessableOrderPageView : ContentPage
{
	public ProcessableOrderPageView(ProcessableOrderModelView model)
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
            await (BindingContext as ProcessableOrderModelView).CancelAsync();
            await Navigation.PopAsync();
        };

        var aprove = new Button
        {
            Text = "Отправлен",
            WidthRequest = 150,
            Margin = 20
        }.Bind(IsVisibleProperty, "AproveVisible");

        aprove.Clicked += async (o, e) =>
        {
            await (BindingContext as ProcessableOrderModelView).AproveAsync();
            await Navigation.PopAsync();
        };

        var reciev = new Button
        {
            Text = "Получен",
            WidthRequest = 150,
            Margin = 20
        }.Bind(IsVisibleProperty, "RecieveVisible");

        reciev.Clicked += async (o, e) =>
        {
            await (BindingContext as ProcessableOrderModelView).RecieveyAsync();
            await Navigation.PopAsync();
        };

        var ready = new Button
        {
            Text = "Готов",
            WidthRequest = 150,
            Margin = 20
        }.Bind(IsVisibleProperty, "ReadyVisible");

        ready.Clicked += async (o, e) =>
        {
            await (BindingContext as ProcessableOrderModelView).ReadyAsync();
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
            RowDefinitions = Rows.Define(Auto, Auto, Auto),
            ColumnDefinitions = Columns.Define(Auto, Auto),
            Children =
            {
                state.ColumnSpan(2)
                ,aprove.Row(1),cancel.Column(1).Row(1)
                ,ready.Row(2),reciev.Column(1).Row(2)
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
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Product.Item.Name");
                var provider = new Label
                {

                }.Bind(Label.TextProperty, "Product.Provider.Name"); ;
                var quantity = new Label
                {

                }.Bind(Label.TextProperty, "Quantity");
                var cost = new Label
                {

                }.Bind(Label.TextProperty, "SumCost", stringFormat: "{0:0.00}");

                var layout = new Grid
                {
                    Padding = 10,
                    RowDefinitions = Rows.Define(Auto, Auto, Auto),
                    ColumnDefinitions = Columns.Define(Auto, Auto, Auto, 60, Auto),
                    Children =
                    {
                        name.ColumnSpan(2),
                        provider.ColumnSpan(2).Row(1),
                        cost.RowSpan(2).Column(4),
                        quantity.Row(2).Column(1)
                    }
                };

                return layout;

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

    protected override void OnNavigatedTo(NavigatedToEventArgs args) => (BindingContext as ProcessableOrderModelView).Update();
}