using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages;

public class BasketPageView : ContentPage
{
	public BasketPageView(BasketViewModel model)
	{
        BindingContext = model;
        var button = new Button
        {
            Text = "Order",
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Start
        };

        button.Clicked += async (o, e) => await (BindingContext as BasketViewModel).CreateAsync();

        var view = new CollectionView
        {
            Header = new Label
            {
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                Text = "Корзина"
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
                    Text = "Удалить",
                    Command = (BindingContext as BasketViewModel).DeleteCommand
                }.Bind(Button.CommandParameterProperty, Binding.SelfPath);

                var layout = new VerticalStackLayout
                {
                    Padding = 20,
                    Children = { product, provider, quantity, add, remove, delete }
                };

                return layout;
            }
        };

		Content = new VerticalStackLayout
		{
			Children = {
				button, view
			}
		};
	}
}