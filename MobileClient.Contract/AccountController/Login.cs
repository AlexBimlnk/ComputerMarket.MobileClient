using Newtonsoft.Json;

namespace MobileClient.Contract.AccountController;
public class Login
{
    /// <summary xml:lang = "ru">
    /// Логин пользователя.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; } = default!;

    /// <summary xml:lang = "ru">
    /// Пароль пользователя.
    /// </summary>
    [JsonProperty("password")]
    public string Password { get; set; } = default!;
}