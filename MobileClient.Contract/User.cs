using Newtonsoft.Json;

namespace MobileClient.Contract;
public class User
{
    /// <summary xml:lang = "ru">
    /// Тип пользователя.
    /// </summary>
    [JsonProperty("type")]
    public UserType Type { get; set; }

    /// <inheritdoc/>
    [JsonProperty("key")]
    public ID Key { get; set; }

    /// <summary xml:lang = "ru">
    /// Данные для ауентификации пользователя.
    /// </summary>
    [JsonProperty("authenticationData")]
    public AuthenticationData AuthenticationData { get; set; }
}
