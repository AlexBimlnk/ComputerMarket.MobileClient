using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class PropertyGroup
{
    /// <summary xml:lang = "ru">
    /// Идентифкатор группы.
    /// </summary>
    [JsonProperty("id")]
    public ID Id { get; }

    /// <summary xml:lang = "ru">
    /// Название группы.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; }
}
