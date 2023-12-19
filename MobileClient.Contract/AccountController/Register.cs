using Newtonsoft.Json;

namespace MobileClient.Contract.AccountController;

public class Register
{
    /// <summary xml:lang = "ru">
    /// Логин пользователя.
    /// </summary>
    [JsonProperty("login")]
    public string Login { get; set; } = default!;

    /// <summary xml:lang = "ru">
    /// Электронная почта пользователя.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; } = default!;

    /// <summary xml:lang = "ru">
    /// Пароль пользователя.
    /// </summary>
    [JsonProperty("password")]
    public string Password { get; set; } = default!;

    /// <summary xml:lang = "ru">
    /// Повторенный пароль пользователя.
    /// </summary>
    [JsonProperty("confirmPassword")]
    public string ConfirmPassword { get; set; } = default!;
}