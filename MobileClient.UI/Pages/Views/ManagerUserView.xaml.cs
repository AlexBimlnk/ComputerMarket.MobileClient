using MobileClient.Contract.Products;
using MobileClient.Logic.Account;
using MobileClient.UI.Helpers;


namespace MobileClient.UI.Pages.Views;

public partial class ManagerUserView : ContentView
{
    private readonly ISignInManager _manager;
    public ManagerUserView()
	{
        _manager = ServiceHelper.GetService<ISignInManager>();
        InitializeComponent();

        BindingContext = this;
	}

    private async void SaveButtonClickAsync(object sender, EventArgs e)
    {
        
    }
}