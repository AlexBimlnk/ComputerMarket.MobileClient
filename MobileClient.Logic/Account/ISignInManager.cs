using MobileClient.Contract;
using MobileClient.Contract.AccountController;

namespace MobileClient.Logic.Account;

public interface ISignInManager
{
    public Task<User?> GetCurrentUserAsync();

    public Task RegisterAsync(Register model);

    public Task LoginAsync(Login model);

    public Task LogOutAsync();
}
