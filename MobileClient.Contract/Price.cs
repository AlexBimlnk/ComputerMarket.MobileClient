using Newtonsoft.Json;

namespace MobileClient.Contract;

public sealed class Price
{
    /// <summary xml:lang = "ru">
    /// Значение.
    /// </summary>
    [JsonProperty("value")]
    public decimal Value { get; set; }
}
