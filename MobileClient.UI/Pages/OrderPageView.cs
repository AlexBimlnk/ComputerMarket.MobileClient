using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

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
            WidthRequest = 200,
        }.Bind(IsVisibleProperty, "CancelVisible");

        cancel.Clicked += async (o, e) =>
        {
            await (BindingContext as OrderViewModel).CancelAsync();
            await Navigation.PopAsync();
        };

        var pay = new Button
        {
            Text = "Оплата",
            WidthRequest = 200
        }.Bind(IsVisibleProperty, "PayVisible");

        pay.Clicked += async (o, e) =>
        {
            await (BindingContext as OrderViewModel).PayAsync();
            await Navigation.PopAsync();
        };

        var label = new Label
        {
            FontSize = 18,
            FontAttributes = FontAttributes.Bold
        }.Bind(Label.TextProperty, "Order.Date", stringFormat: "Заказ от {0}");

        var state = new Label
        {
            FontAttributes = FontAttributes.Italic
        }.Bind(Label.TextProperty, "Order.State");

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
                    Children = {name, provider, quantity, cost}
                };

            }
        };

        Content = new VerticalStackLayout
		{
			Children = {
				label,
                pay, cancel,
                state, list
			}
		};
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args) => (BindingContext as OrderViewModel).Update();
}