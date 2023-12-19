using Newtonsoft.Json;

namespace MobileClient.Contract.Links;
public sealed class Link
{
    /// <summary xml:lang = "ru">
    /// Внешний идентификатор.
    /// </summary>
    [JsonProperty("internalId")]
    public ID InternalID { get; set; }

    /// <summary xml:lang = "ru">
    /// Внутренний идентификатор.
    /// </summary>
    [JsonProperty("externalId")]
    public ID ExternalID { get; set; }

    /// <summary xml:lang = "ru">
    /// Идентификатор поставщика.
    /// </summary>
    [JsonProperty("providerId")]
    public ID ProviderID { get; set; }
}
