using System.Text;

using Microsoft.Extensions.Options;

using MobileClient.Contract.AccountController;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic.Account;

public sealed class LoginHandler : ILoginHandler
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;
    private readonly ISerializer<Login, string> _loginSerializer;
    private readonly ISerializer<Register, string> _registerSerializer;

    public LoginHandler(
        IHttpClientFacade httpClientFacade,
        ISerializer<Login, string> loginSerializer,
        ISerializer<Register, string> registerSerializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));
        _loginSerializer = loginSerializer ?? throw new ArgumentNullException(nameof(loginSerializer));
        _registerSerializer = registerSerializer ?? throw new ArgumentNullException(nameof(registerSerializer));
    }


    public async Task Register(Register register)
    {
        ArgumentNullException.ThrowIfNull(register);

        var result = await _httpClientFacade.PostAsync(
                $"{_serviceConfig.MarketService}/account/api/register",
                new StringContent(_registerSerializer.Serialize(register), Encoding.UTF8, "application/json"))
            .ConfigureAwait(false);

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task LogInAsync(Login login)
    {
        ArgumentNullException.ThrowIfNull(login);

        var result = await _httpClientFacade.PostAsync(
                $"{_serviceConfig.MarketService}/account/api/login",
                new StringContent(_loginSerializer.Serialize(login), Encoding.UTF8, "application/json"))
            .ConfigureAwait(false);

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task LogOutAsync()
    {
        var result = await _httpClientFacade.PostAsync(
                $"{_serviceConfig.MarketService}/account/api/logout", null!)
            .ConfigureAwait(false);

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }
}
