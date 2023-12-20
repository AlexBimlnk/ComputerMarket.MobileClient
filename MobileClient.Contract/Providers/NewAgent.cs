using Newtonsoft.Json;

namespace MobileClient.Contract.Providers;
public sealed class NewAgent
{
    /// <summary xml:lang = "ru">
    /// Логин пользователя.
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; } = null!;
}
