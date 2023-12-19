using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class ItemProperty
{
    /// <summary xml:lang = "ru">
    /// Название свойства.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary xml:lang = "ru">
    /// Значения свойства.
    /// </summary>
    [JsonProperty("value")]
    public string? Value { get; set; }

    /// <summary xml:lang = "ru">
    /// Группа свойства.
    /// </summary>
    [JsonProperty("group")]
    public PropertyGroup Group { get; set; }

    /// <summary xml:lang = "ru">
    /// Используется ли при фильтрации.
    /// </summary>
    [JsonProperty("isFilterable")]
    public bool IsFilterable { get; set; }

    /// <summary xml:lang = "ru">
    /// Ключ свойства.
    /// </summary>
    [JsonProperty("key")]
    public ID Key { get; set; }

    /// <summary xml:lang = "ru">
    /// Тип данных, который хранится в свойстве.
    /// </summary>
    [JsonProperty("propertyDataType")]
    public int DataType { get; set; }

    public PropertyDataType PropertyDataType => (PropertyDataType)DataType;
}
