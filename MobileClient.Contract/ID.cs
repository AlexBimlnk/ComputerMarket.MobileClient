using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class ID
{
    [JsonProperty("value")]
    public long Value { get; set; }
}