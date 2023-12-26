using MobileClient.Contract.AccountController;
using MobileClient.Contract;
using MobileClient.UI.Helpers;
using MobileClient.Logic.Account;

namespace MobileClient.UI.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly ISignInManager _manager;

    public RegisterPage()
    {
        _manager = ServiceHelper.GetService<ISignInManager>();
        InitializeComponent();
    }

    private async void SaveButtonClickedAsync(object sender, EventArgs e)
    {
        User? user = null;
        try
        {
            await _manager.RegisterAsync(new Register()
            {
                Login = loginEntry.Text,
                Email = emailEntry.Text,
                Password = passwordEntry.Text,
                ConfirmPassword = passwordEntry.Text
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