using Newtonsoft.Json;

namespace MobileClient.Contract.Products;
public class FilterProperty
{
    /// <inheritdoc/>
    [JsonProperty("property")]
    public ItemProperty Property { get; set; }

    /// <inheritdoc/>
    [JsonProperty("values")]
    public IReadOnlyCollection<FilterValue> Values { get; set; }
}
