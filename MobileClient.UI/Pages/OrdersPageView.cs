
using CommunityToolkit.Maui.Markup;

using MobileClient.Contract.Orders;
using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages;

public class OrdersPageView : ContentPage
{
	public OrdersPageView(OrdersViewModel model)
	{
        BindingContext = model;
        var view = new CollectionView
        {
            SelectionMode = SelectionMode.Single,
            Header = new Label
            {
                Text = "Заказы",
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            }
        }.ItemsSource(model.Orders);

        view.SelectionChanged += async (o, e) =>
        {
            if (e.CurrentSelection.FirstOrDefault() is not Order item)
                return;
            await Shell.Current.GoToAsync(nameof(OrderPageView), true, new Dictionary<string, object>
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
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Key.Value");
                var state = new Label
                {
                    VerticalOptions = LayoutOptions.End
                }.Bind(Label.TextProperty, "State");
                var date = new Label
                {
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "OrderDate");
                var sum = new Label 
                {
                    VerticalOptions = LayoutOptions.End
                }.Bind(Label.TextProperty, "Sum");

                var item = new VerticalStackLayout
                {
                    Children = {key, state, date, sum}
                };

                return item;
            }
        };

		Content = new VerticalStackLayout
		{
			Children = {
				view
			}
		};
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as OrdersViewModel).ReloadDataAsync();
}