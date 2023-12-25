namespace MobileClient.Logic.Account;

public interface ISignInManager
{
    public bool IsLoggedIn { get; set; }

    public Task RegisterAsync() => throw new NotImplementedException();

    public Task LoginAsync() => throw new NotImplementedException();

    public Task LogOutAsync() => throw new NotImplementedException();
}
