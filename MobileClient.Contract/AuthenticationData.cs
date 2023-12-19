using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class AuthenticationData
{
    /// <summary xml:lang = "ru">
    /// Логин пользователя.
    /// </summary>
    [JsonProperty("login")]
    public string Login { get; set; }

    /// <summary xml:lang = "ru">
    /// Email.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    [JsonProperty("password")]
    public Password Password { get; set; }
}
