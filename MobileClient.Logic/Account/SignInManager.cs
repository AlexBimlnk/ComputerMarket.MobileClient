namespace MobileClient.Logic.Account;

public sealed class SignInManager : ISignInManager
{
    private static bool s_state = false;
    private readonly ILoginHandler _loginHandler;

    public bool IsLoggedIn { get => s_state; set => s_state = value; }

    public SignInManager(ILoginHandler loginHandler)
    {
        _loginHandler = loginHandler;
    }

    public Task RegisterAsync() => throw new NotImplementedException();

    public Task LoginAsync() => throw new NotImplementedException();

    public Task LogOutAsync() => throw new NotImplementedException();
}
