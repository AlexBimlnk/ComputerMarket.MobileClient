using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class PaymentTransactionsInformation
{
    /// <summary xml:lang = "ru">
    /// Инн поставщика.
    /// </summary>
    [JsonProperty("inn")]
    public string INN { get; set; }

    /// <summary xml:lang = "ru">
    /// Банковский счёт поставщика.
    /// </summary>
    [JsonProperty("bankAccount")]
    public string BankAccount { get; set; }
}
