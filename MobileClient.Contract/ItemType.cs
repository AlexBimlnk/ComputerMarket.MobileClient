using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class ItemType
{
    /// <summary xml:lang = "ru">
    /// Название типа.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; }

    /// <summary xml:lang = "ru">
    /// Индетификатор типа продукта.
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; }

    /// <inheritdoc/>
    [JsonProperty("url")]
    public string? URL { get; }
}
