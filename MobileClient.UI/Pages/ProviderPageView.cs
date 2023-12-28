using CommunityToolkit.Maui.Markup;

using MobileClient.Contract;
using MobileClient.UI.Pages.Models;

using static System.Net.Mime.MediaTypeNames;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MobileClient.UI.Pages;

[QueryProperty("Provider", "Provider")]
public class ProviderPageView : ContentPage
{
    public ProviderPageView(ProviderViewModel model)
    {
        BindingContext = model;
        Title = "Поставщик";

        var label = new Grid
        {
            Padding = 10,
            Margin = 10,
            ColumnDefinitions = Columns.Define(Auto, Auto),
            Children = 
            {
                new Label
                {
                    Margin = 10,
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold
                }.Bind(Label.TextProperty, "Provider.Name"),
                new Label 
                {
                    Margin = 10,
                    FontSize = 18
                }.Bind(Label.TextProperty, "Provider.ApproveState").Column(1)
            }
        };

        var entry = new Entry
        {
            Margin = 10,
            FontSize = 15
        };

        var data = new Grid
        {
            Padding = 10,
            Margin = 10,
            RowDefinitions = Rows.Define(Auto, Auto, Auto),
            ColumnDefinitions = Columns.Define(Auto, Auto),
            Children =
            {
                new Label 
                {
                    Text = "ИНН: ",
                    Margin = 10,
                    FontSize = 15
                },
                new Label
                {
                    Margin = 10,
                    FontSize = 15,
                }
                .Bind(Label.TextProperty, "Provider.PaymentTransactionsInformation.INN")
                .Column(1),
                new Label
                {
                    Text = "Счёт: ",
                    Margin = 10,
                    FontSize = 15
                }.Row(1),
                new Label
                {
                    Margin = 10,
                    FontSize = 15
                }.Bind(Label.TextProperty, "Provider.PaymentTransactionsInformation.BankAccount").Row(1).Column(1),
                new Label
                {
                    Text = "Маржа: ",
                    Margin = 10,
                    FontSize = 15
                }.Row(2),
                entry.Row(2).Column(1)
            }
        };


        var save = new Button
        {
            Margin = 20,
            Text = "Сохранить",
            WidthRequest = 150
        };

        var aprove = new Button
        {
            Margin = 20,
            Text = "Подтвердить",
            WidthRequest = 150
        }.Bind(IsVisibleProperty, "IsNeedAprove");

        save.Clicked += async (o, e) =>
        {
            await (BindingContext as ProviderViewModel).SaveInfoAsync();
            await Navigation.PopAsync();
        };
        aprove.Clicked += async (o, e) =>
        {
            await (BindingContext as ProviderViewModel).AproveProviderAsync();
            await Navigation.PopAsync();
        };

        Content = new VerticalStackLayout
        {
            Children = {
                label, data, aprove, save
            }
        };
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args) => (BindingContext as ProviderViewModel).Update();
}