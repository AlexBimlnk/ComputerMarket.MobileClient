using System.ComponentModel;
using System.Windows.Input;

using CommunityToolkit.Maui.Markup;

using MobileClient.Logic.Account;

namespace MobileClient.UI.Pages;

public partial class ProfilePage : ContentPage, INotifyPropertyChanged
{
    private readonly ISignInManager _signInManager;

	public ProfilePage(ISignInManager manager)
	{
		InitializeComponent();
        _signInManager = manager;
        BindingContext = this;
	}

    protected async override void OnNavigatedTo(NavigatedToEventArgs args) {
        var user = await _signInManager.GetCurrentUserAsync() ?? throw new Exception();
        if (user.Type == Contract.UserType.Manager)
        {
            PositionSelected = 0;
        }
        else
        {
            PositionSelected = 1;
        }
    }


    public ICommand SelectItemCommand => new Command<string>((param) => PositionSelected = int.Parse(param));

    public int _positionSelected = 0;

    public int PositionSelected
    {
        set
        {
            if (_positionSelected != value)
            {
                _positionSelected = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PositionSelected)));
            }
        }
        get => _positionSelected;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}