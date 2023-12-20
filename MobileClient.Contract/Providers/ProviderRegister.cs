using Newtonsoft.Json;

namespace MobileClient.Contract.Providers;
public class ProviderRegister
{
    /// <summary xml:lang = "ru">
    /// Название поставщика.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    /// <summary xml:lang = "ru">
    /// Счёт поставщика.
    /// </summary>
    [JsonProperty("bankAccount")]
    public string BankAccount { get; set; } = default!;

    /// <summary xml:lang = "ru">
    /// ИНН поставщика.
    /// </summary>
    [JsonProperty("inn")]
    public string INN { get; set; } = default!;
}
