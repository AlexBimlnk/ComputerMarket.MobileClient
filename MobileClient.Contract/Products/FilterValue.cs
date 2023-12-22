using Newtonsoft.Json;

namespace MobileClient.Contract.Products;
public sealed class FilterValue
{
    /// <inheritdoc/>
    [JsonProperty("propertyId")]
    public ID PropertyID { get; set; }

    /// <inheritdoc/>
    [JsonProperty("value")]
    public string Value { get; set; }

    /// <inheritdoc/>
    [JsonProperty("selected")]
    public bool Selected { get; set; }
}
