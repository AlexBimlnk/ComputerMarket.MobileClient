using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

public class BasketPageView : ContentPage
{
    public BasketPageView(BasketViewModel model)
    {
        Title = "Корзина";
        BindingContext = model;
        var button = new Button
        {
            Text = "Создать",
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Start,
            Margin = 30,
            FontSize = 15

        };
        var separator = new BoxView
        {
            Color = Colors.LightGray,
            HeightRequest = 1,
            Margin = 20
        };
        button.Clicked += async (o, e) => await (BindingContext as BasketViewModel).CreateAsync();

        var view = new CollectionView
        {
            EmptyView = new Label
            {
                FontSize = 20,
                Text = "Корзина пустая"
            }
        }.ItemsSource((BindingContext as BasketViewModel).Products);

        view.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var product = new Label
                {
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Product.Item.Name");
                var provider = new Label
                {

                }.Bind(Label.TextProperty, "Product.Provider.Name");
                var quantity = new Label
                {

                }.Bind(Label.TextProperty, "Quantity");
                var cost = new Label
                {

                }.Bind(Label.TextProperty, "SumCost", stringFormat: "{0:0.00} ₸");
                var add = new Button
                {
                    Text = "+",
                    Command = (BindingContext as BasketViewModel).AddCommand
                }.Bind(Button.CommandParameterProperty, Binding.SelfPath);
                var remove = new Button
                {
                    Text = "-",
                    Command = (BindingContext as BasketViewModel).RemoveCommand
                }.Bind(Button.CommandParameterProperty, Binding.SelfPath);
                var delete = new Button
                {
                    Text = "X",
                    Command = (BindingContext as BasketViewModel).DeleteCommand
                }.Bind(Button.CommandParameterProperty, Binding.SelfPath);

                var layout = new Grid
                {
                    Padding = 10,
                    RowDefinitions = Rows.Define(Auto, Auto, Auto),
                    ColumnDefinitions = Columns.Define(Auto, Auto, Auto, 60, Auto),
                    Children =
                    {
                        product.ColumnSpan(2),
                        provider.ColumnSpan(2).Row(1),
                        cost.RowSpan(2).Column(4),
                        add.Row(2),
                        quantity.Row(2).Column(1),
                        remove.Row(2).Column(2),
                        delete.Row(2).Column(4)

                    }
                };

                return layout;
            }
        };

        Content = new VerticalStackLayout
        {
            Children = {
                button, separator,new ScrollView
                {
                    Content = view
                }

            }
        };
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as BasketViewModel).ReloadDataAsync();
}