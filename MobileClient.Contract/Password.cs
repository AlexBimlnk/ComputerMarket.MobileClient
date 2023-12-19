using Newtonsoft.Json;

namespace MobileClient.Contract;
public class Password
{
    /// <summary xml:lang = "ru">
    /// Значение.
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; set; }
}
