using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class Item
{
    /// <summary xml:lang = "ru">
    /// Тип товара.
    /// </summary>
    [JsonProperty("type")]
    public ItemType Type { get; }

    /// <summary xml:lang = "ru">
    /// Название товара.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; }

    /// <summary xml:lang = "ru">
    /// Характеристики товара.
    /// </summary>
    [JsonProperty("properties")]
    public IReadOnlyCollection<ItemProperty> Properties { get; }

    [JsonProperty("key")]
    public ID Key { get; }

    [JsonProperty("url")]
    public string? URL { get; }
}
