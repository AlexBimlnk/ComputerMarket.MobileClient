using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class Item
{
    /// <summary xml:lang = "ru">
    /// Тип товара.
    /// </summary>
    [JsonProperty("type")]
    public ItemType Type { get; set; }

    /// <summary xml:lang = "ru">
    /// Название товара.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary xml:lang = "ru">
    /// Характеристики товара.
    /// </summary>
    [JsonProperty("properties")]
    public IReadOnlyCollection<ItemProperty> Properties { get; set; }

    [JsonProperty("key")]
    public ID Key { get; set; }

    [JsonProperty("url")]
    public string? URL { get; set; }
}
