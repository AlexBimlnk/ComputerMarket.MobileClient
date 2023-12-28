using CommunityToolkit.Maui.Markup;

using MobileClient.Contract;
using MobileClient.UI.Pages.Models;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

public class ProvidersPageView : ContentPage
{
    public ProvidersPageView(ProvidersViewModel model)
    {
        Title = "Поставщики";
        BindingContext = model;

        var view = new CollectionView
        {
            SelectionMode = SelectionMode.Single
        }.ItemsSource((BindingContext as ProvidersViewModel).Items);

        view.SelectionChanged += async (o, e) =>
        {
            if (e.CurrentSelection.FirstOrDefault() is not Provider item)
                return;
            await Shell.Current.GoToAsync(nameof(ProviderPageView), true, new Dictionary<string, object>
            {
                ["Provider"] = item
            });
        };

        view.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var key = new Label
                {
                    Margin = 10,
                    FontSize = 15
                }.Bind(Label.TextProperty, "Key.Value");

                var name = new Label
                {
                    Margin = 10,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Name");

                var margin = new Label
                {
                    Margin = 10,
                    FontSize = 18,
                }.Bind(Label.TextProperty, "Margin.Value", stringFormat: "{0:P2}");

                var isAproved = new Label
                {
                    Margin = 10,
                    FontSize = 15
                }.Bind(Label.TextProperty, "ApproveState");

                var edit = new Button
                {
                    Margin = 10,
                    Command = model.ItemChangedCommand,
                    WidthRequest = 150,
                    Text = "Посмотреть"
                }.Bind(Button.CommandParameterProperty, Binding.SelfPath);

                var agent = new Button
                {
                    Margin = 10,
                    Command = model.GoTo,
                    WidthRequest = 150,
                    Text = "Представители"
                }.Bind(Button.CommandParameterProperty, Binding.SelfPath).Bind(IsVisibleProperty, "IsAproved");

                var layout = new Grid
                {
                    Padding = 10,
                    RowDefinitions = Rows.Define(Auto, Auto, Auto, Auto),
                    ColumnDefinitions = Columns.Define(Auto, Auto, Auto, Auto),
                    Children =
                    {
                        key,
                        name.Column(1),
                        isAproved.Column(3),
                        margin.Row(1).ColumnSpan(4),
                        edit.Row(2).ColumnSpan(4),
                        agent.Row(3).ColumnSpan(4),
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

    protected override async void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as ProvidersViewModel).ReloadDataAsync();
}