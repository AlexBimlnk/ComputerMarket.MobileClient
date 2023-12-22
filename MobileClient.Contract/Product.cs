using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class Product
{
    /// <summary xml:lang = "ru">
    /// Описание продукта.
    /// </summary>
    [JsonProperty("item")]
    public Item Item { get; set; }

    /// <summary xml:lang = "ru">
    /// Цена назначаенная поставщиком.
    /// </summary>
    [JsonProperty("providerCost")]
    public decimal ProviderCost { get; set; }

    /// <summary xml:lang = "ru">
    /// Итоговая цена продукта.
    /// </summary>
    [JsonProperty("finalCost")]
    public decimal FinalCost { get; set; }

    /// <summary xml:lang = "ru">
    /// Поставщик продукта.
    /// </summary>
    [JsonProperty("provider")]
    public Provider Provider { get; set; }

    /// <summary xml:lang = "ru">
    /// Количестов продукта.
    /// </summary>
    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    /// <inheritdoc/>
    [JsonProperty("key")]
    public (ID, ID) Key { get; set; }
}