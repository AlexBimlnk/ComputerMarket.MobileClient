using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages;

public class ProviderRegisterPageView : ContentPage
{
    public ProviderRegisterPageView(ProviderRegisterViewModel model)
    {
        BindingContext = model;
        Title = "Новый поставщик";
        var register = new Button
        {
            Text = "Зарегистрироваться"
        };

        register.Clicked += async (o, e) =>
        {
            await (BindingContext as ProviderRegisterViewModel).RegisterAsync();
            await Navigation.PopAsync();
        };

        var name = new Entry
        {
            Placeholder = "Название"
        }
            .Bind(Entry.TextProperty,
                getter: static (ProviderRegisterViewModel vm) => vm.Name,
                setter: static (ProviderRegisterViewModel vm, string code) => vm.Name = code
            );

        var inn = new Entry
        {
            Placeholder = "ИНН"
        }
            .Bind(Entry.TextProperty,
                getter: static (ProviderRegisterViewModel vm) => vm.INN,
                setter: static (ProviderRegisterViewModel vm, string code) => vm.INN = code
            );

        var account = new Entry
        {
            Placeholder = "Счёт"
        }
            .Bind(Entry.TextProperty,
                getter: static (ProviderRegisterViewModel vm) => vm.Account,
                setter: static (ProviderRegisterViewModel vm, string code) => vm.Account = code
            );

        Content = new VerticalStackLayout
        {
            Children = {
                name, inn, account, register
            }
        };
    }
}