using Newtonsoft.Json;

namespace MobileClient.Contract.Products;
public sealed class Catalog
{
    [JsonProperty("searchString")]
    public string? SearchString { get; set; }

    [JsonProperty("typeId")]
    public int? TypeId { get; set; }

    [JsonProperty("params")]
    public string? Params { get; set; }

    [JsonProperty("products")]
    public IEnumerable<Product> Products { get; set; }

    [JsonProperty("properties")]
    public IReadOnlyDictionary<string, FilterProperty> Properties { get; set; } //string = ID { Value = 1 }
}
