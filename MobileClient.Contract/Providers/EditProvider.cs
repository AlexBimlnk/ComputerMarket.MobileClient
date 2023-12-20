using Newtonsoft.Json;

namespace MobileClient.Contract.Providers;
public sealed class EditProvider
{
    /// <summary xml:lang = "ru">
    /// Идентификатор провайдера.
    /// </summary>
    [JsonProperty("key")]
    public long Key { get; set; }

    /// <summary xml:lang = "ru">
    /// Имя провайдера.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// ИНН поставщика.
    /// </summary>
    [JsonProperty("inn")]
    public string INN { get; set; } = null!;

    /// <summary>
    /// Банковский счёт поставщика.
    /// </summary>
    [JsonProperty("bankAccount")]
    public string BankAccount { get; set; } = null!;

    /// <summary xml:lang = "ru">
    /// Маржа провайдера.
    /// </summary>
    [JsonProperty("margin")]
    public string Margin { get; set; } = default!;

    /// <summary>
    /// Подтверждён ли провайдер.
    /// </summary>
    [JsonProperty("isApproved")]
    public bool IsAproved { get; set; }
}
