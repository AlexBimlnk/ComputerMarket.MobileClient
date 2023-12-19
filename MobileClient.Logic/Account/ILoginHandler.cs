using MobileClient.Contract.AccountController;

namespace MobileClient.Logic.Account;
public interface ILoginHandler
{
    Task LogInAsync(Login login);
    Task LogOutAsync();
    Task Register(Register register);
}