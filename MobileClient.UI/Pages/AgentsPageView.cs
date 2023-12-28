using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

using Org.Apache.Http.Authentication;

using Xamarin.Google.Crypto.Tink.Subtle;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

[QueryProperty("Provider", "Provider")]
public class AgentsPageView : ContentPage
{
    public AgentsPageView(AgentsViewModel model)
    {
        Title = "Представители";
        BindingContext = model;
        var add = new Button
        {
            Margin = 10,
            Text = "Добавить",
            WidthRequest = 200
        };

        var label = new Label
        {
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            Margin = 10
        }.Bind(Label.TextProperty, "Provider.Name", stringFormat: "Представители {0}");

        var data = new CollectionView()
        {
            EmptyView = new Label
            {
                FontSize = 20,
                Text = "У поставщика нет представителей"
            }
        }.ItemsSource(model.Items);

        data.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var user = new Label
                {
                    FontSize = 15,
                    Margin = 10
                }.Bind(Label.TextProperty, "AuthenticationData.Login");

                var email = new Label
                {
                    FontSize = 15,
                    Margin = 10
                }.Bind(Label.TextProperty, "AuthenticationData.Email");

                var button = new Button
                {
                    Text = "X",
                    FontSize = 15,
                    Margin = 10,
                    Command = model.ItemChangedCommand
                }.Bind(Button.CommandParameterProperty, Binding.SelfPath);

                var label = new Grid
                {
                    Padding = 10,
                    Margin = 10,
                    ColumnDefinitions = Columns.Define(Auto, Auto, Auto),
                    Children =
                    {
                        user, email.Column(1), button.Column(2)
                    }
                };

                return label;
            }
        };

        add.Clicked += async (o, e) => await model.AddNewAsync();

        Content = new VerticalStackLayout
        {
            Children =
            {
                label, add, data
            }
        };
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args) => base.OnNavigatedTo(args);
}