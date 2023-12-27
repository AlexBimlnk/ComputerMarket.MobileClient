using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages;

public class ProviderRegisterPageView : ContentPage
{
    public ProviderRegisterPageView(ProviderRegisterViewModel model)
    {
        BindingContext = model;
        Title = "����� ���������";
        var register = new Button
        {
            Text = "������������������"
        };

        register.Clicked += async (o, e) =>
        {
            await (BindingContext as ProviderRegisterViewModel).RegisterAsync();
            await Navigation.PopAsync();
        };

        var name = new Entry
        {
            Placeholder = "��������"
        }
            .Bind(Entry.TextProperty,
                getter: static (ProviderRegisterViewModel vm) => vm.Name,
                setter: static (ProviderRegisterViewModel vm, string code) => vm.Name = code
            );

        var inn = new Entry
        {
            Placeholder = "���"
        }
            .Bind(Entry.TextProperty,
                getter: static (ProviderRegisterViewModel vm) => vm.INN,
                setter: static (ProviderRegisterViewModel vm, string code) => vm.INN = code
            );

        var account = new Entry
        {
            Placeholder = "����"
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