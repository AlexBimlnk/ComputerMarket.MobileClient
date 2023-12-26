using System.Reflection;

using MobileClient.Contract;
using MobileClient.Contract.AccountController;
using MobileClient.Logic.Data;
using MobileClient.Logic.Data.Models;

namespace MobileClient.Logic.Account;

public sealed class SignInManager : ISignInManager
{
    private readonly ILoginHandler _loginHandler;
    private readonly IInnerDatabase _database;

    private static UserModel ToStorage(User model)
    {
        var result = new UserModel()
        {
            UserId = model.Key.Value,
            Email = model.AuthenticationData.Email,
            Login = model.AuthenticationData.Login,
            Password = model.AuthenticationData.Password.Value,
            UserType = (int)model.Type
        };

        return result;
    }

    private static User ToModel(UserModel storage)
    {
        var result = new User()
        {
            Key = new ID()
            {
               Value = storage.Id
            },
            Type = (UserType)storage.UserType,
            AuthenticationData = new AuthenticationData() 
            {
                Email = storage.Email,
                Login = storage.Login,
                Password = new Password()
                {
                    Value = storage.Password
                }
            }
            
        };

        return result;
    }

   
        

    public SignInManager(ILoginHandler loginHandler, IInnerDatabase database)
    {
        _loginHandler = loginHandler;
        _database = database;
    }

    private async Task ClearAsync()
    {
        foreach (var item in await _database.GetItemsAsync())
        {
            if (item is null)
            {
                continue;
            }
            await _database.DeleteItemAsync(item);
        }
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var first = (await _database.GetItemsAsync()).FirstOrDefault();
        if (first is null)
        {
            return null;
        }

        await LoginAsync(new Login()
        {
            Email = first.Email,
            Password = first.Password
        });

        return ToModel(first);
    }

    public async Task RegisterAsync(Register model)
    {
        var user = await _loginHandler.Register(model);

        await LoginAsync(new Login()
        {
            Email = user.AuthenticationData.Email,
            Password = user.AuthenticationData.Password.Value
        });

    }

    public async Task LoginAsync(Login model)
    {
        var user = await _loginHandler.LogInAsync(model);

        await ClearAsync();
        await _database.SaveItemAsync(ToStorage(user));
    }

    public async Task LogOutAsync()
    {
        await _loginHandler.LogOutAsync();
        await ClearAsync();
    }
}
