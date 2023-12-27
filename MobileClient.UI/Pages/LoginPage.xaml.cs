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

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        //await _manager.LoginAsync(new Login()
        //{
        //    Email = "manager@mail.ru",
        //    Password = "12345678"
        //});
        //IsVisible = false;
        var a = 2;
        await (Shell.Current as AppShellMobile).CheckUserAsync();
    }

    private async void RegisterClikcAsync(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(RegisterPage), true, new Dictionary<string, object>());
}