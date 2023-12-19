using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class Provider
{
    /// <summary xml:lang = "ru">
    ///  Название поставщика.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary xml:lang = "ru">
    /// Заданная маржа поставщика.
    /// </summary>
    [JsonProperty("margin")]
    public Margin Margin { get; set; }

    /// <summary xml:lang = "ru">
    /// Дополнительная информация об поставщике.
    /// </summary>
    [JsonProperty("paymentTransactionsInformation")]
    public PaymentTransactionsInformation PaymentTransactionsInformation { get; set; }

    /// <summary xml:lang="ru">
    /// Подтвержден ли провайдер.
    /// </summary>
    [JsonProperty("isApproved")]
    public bool IsAproved { get; set; }

    /// <inheritdoc/>
    [JsonProperty("key")]
    public ID Key { get; set; }
}
