using MobileClient.Contract;
using MobileClient.Contract.AccountController;
using MobileClient.Logic.Account;
using MobileClient.UI.Helpers;

namespace MobileClient.UI.Pages;

public partial class LoginPage : ContentPage
{
    private readonly ISignInManager _manager;

    public LoginPage()
    {
        _manager = ServiceHelper.GetService<ISignInManager>();
        InitializeComponent();
    }

    private async void SaveButtonClickedAsync(object sender, EventArgs e)
    {
        User? user = null;
        try
        {
            await _manager.LoginAsync(new Login()
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            });

            user = await _manager.GetCurrentUserAsync();
        }
        catch (Exception ex)
        {
            return;
        }

        IsVisible = false;                          
        await (Shell.Current as AppShellMobile).CheckUserAsync();
    }
}