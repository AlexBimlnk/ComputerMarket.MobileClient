using Newtonsoft.Json;

namespace MobileClient.Contract.Builder;
public class BuildResult
{
    [JsonProperty("isSucces")]
    public bool IsSucces { get; set; }

    [JsonProperty("errorsByType")]
    public IReadOnlyDictionary<ItemType, IReadOnlyCollection<string>> ErrorsByType { get; set; }
}
