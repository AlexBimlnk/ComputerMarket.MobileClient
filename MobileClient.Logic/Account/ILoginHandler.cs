using MobileClient.Contract;
using MobileClient.Contract.AccountController;

namespace MobileClient.Logic.Account;
public interface ILoginHandler
{
    Task<User> LogInAsync(Login login);
    Task LogOutAsync();
    Task<User> Register(Register register);
}