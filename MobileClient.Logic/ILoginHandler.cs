using MobileClient.Contract.AccountController;

namespace MobileClient.Logic;
public interface ILoginHandler
{
    Task LogIn(Login login);
    Task LogOut();
    Task Register(Register register);
}