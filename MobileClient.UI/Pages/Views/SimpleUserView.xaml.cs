using MobileClient.Contract.Products;
using MobileClient.Logic.Account;
using MobileClient.UI.Helpers;

namespace MobileClient.UI.Pages.Views;

public partial class SimpleUserView : ContentView
{
    private readonly ISignInManager _manager;
    public SimpleUserView()
	{
        _manager = ServiceHelper.GetService<ISignInManager>();
        InitializeComponent();
        BindingContext = this;
    }

    public string Title { get; set; } = "Place";

    protected async override void OnParentSet()
    {
        var user = await _manager.GetCurrentUserAsync();

        Title = $"Hello, {user!.AuthenticationData.Login}";
    }

    private async void SaveButtonClickAsync(object sender, EventArgs e)
    {
    }
}