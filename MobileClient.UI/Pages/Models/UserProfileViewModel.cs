using System.ComponentModel;
using System.Runtime.CompilerServices;

using MobileClient.Logic.Account;

using Nito.AsyncEx;

namespace MobileClient.UI.Pages.Models;

public class UserProfileViewModel : INotifyPropertyChanged
{
    private readonly ISignInManager _manager;
    public UserProfileViewModel(ISignInManager signInManager)
    {
        _manager = signInManager;
    }

    public AsyncLazy<string> Greeting => new AsyncLazy<string>(GetGreetingAsync);

    public async Task<string> GetGreetingAsync()
    {
        var user = await _manager.GetCurrentUserAsync();
        return $"Привет, {user.AuthenticationData.Login}";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
