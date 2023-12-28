using CommunityToolkit.Maui.Markup;

using MobileClient.Contract;
using MobileClient.UI.Pages.Models;
using Org.Apache.Http.Authentication;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

public class LinksPageView : ContentPage
{
	public LinksPageView(LinksViewModel model)
	{
        BindingContext = model;
        Title = "Связи";
        var buton = new Button
        {
            Text = "Добавить",
            WidthRequest = 200,
            Margin = 20,
        };

        var view = new CollectionView
        {
            Header = new Grid
            {
                Padding = 10,
                ColumnDefinitions = Columns.Define(Auto, Auto, Auto),
                Children =
                {
                    new Label { Text = "Internal", FontSize = 15, FontAttributes = FontAttributes.Bold, Margin = 20},
                    new Label { Text = "External", FontSize = 15, FontAttributes = FontAttributes.Bold , Margin = 20}.Column(1),
                    new Label { Text = "Provider", FontSize = 15, FontAttributes = FontAttributes.Bold , Margin = 20}.Column(2)
                }
            }
        }.ItemsSource(model.Items);

        view.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var intl = new Label { FontSize = 15, Margin = 40 }.Bind(Label.TextProperty, "InternalID.Value");
                var extl = new Label { FontSize = 15, Margin = 40 }.Bind(Label.TextProperty, "ExternalID.Value");
                var prv = new Label {  FontSize = 15, Margin = 40 }.Bind(Label.TextProperty, "ProviderID.Value");

                var grid = new Grid
                {
                    Padding = 10,
                    ColumnDefinitions = Columns.Define(Auto, Auto, Auto),
                    Children =
                    {
                        intl, extl.Column(1), prv.Column(2)
                    }
                };

                return grid;
            }
        };

		Content = new VerticalStackLayout
		{
			Children = { buton, view }
		};
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as LinksViewModel).ReloadDataAsync();
}