using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class Margin
{
    [JsonProperty("value")]
    public decimal Value { get; set; }
}
