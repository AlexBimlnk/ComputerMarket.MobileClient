using Newtonsoft.Json;

namespace MobileClient.Contract.Links;
public sealed class Link
{
    /// <summary xml:lang = "ru">
    /// Внешний идентификатор.
    /// </summary>
    [JsonProperty("internalID")]
    public ID InternalID { get; set; }

    /// <summary xml:lang = "ru">
    /// Внутренний идентификатор.
    /// </summary>
    [JsonProperty("externalID")]
    public ID ExternalID { get; set; }

    /// <summary xml:lang = "ru">
    /// Идентификатор поставщика.
    /// </summary>
    [JsonProperty("providerID")]
    public ID ProviderID { get; set; }
}
