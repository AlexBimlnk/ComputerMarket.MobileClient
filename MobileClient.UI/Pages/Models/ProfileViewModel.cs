using System.ComponentModel;
using System.Windows.Input;

using MobileClient.Logic.Account;

namespace MobileClient.UI.Pages.Models;
public class ProfileViewModel: INotifyPropertyChanged
{
    private readonly ISignInManager _signInManager;
    public ProfileViewModel(ISignInManager manager)
    {
        _signInManager = manager;
    }

    public async Task CheckUserAsync()
    {
        var user = await _signInManager.GetCurrentUserAsync() ?? throw new Exception();
        if (user.Type == Contract.UserType.Manager)
        {
            PositionSelected = 0;
        }
        else if (user.Type == Contract.UserType.Customer)
        {
            PositionSelected = 1;
        }
        else
        {
            PositionSelected = 2;
        }
    }

    public string Greetings { get; set; } = "Привет!";

    public async Task UpdateUserAsync()
    {
        var user = await _signInManager.GetCurrentUserAsync();

        Greetings = $"Привет, {user.AuthenticationData.Login}!";

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Greetings)));
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
