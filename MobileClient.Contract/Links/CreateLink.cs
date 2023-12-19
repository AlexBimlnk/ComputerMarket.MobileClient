using Newtonsoft.Json;

namespace MobileClient.Contract.Links;
public class CreateLink
{
    /// <summary xml:lang = "ru">
    /// Внутренний идентификатор продукта.
    /// </summary>
    [JsonProperty("internalId")]
    public long InternalId { get; set; }

    /// <summary xml:lang = "ru">
    /// Идентификатор поставщика.
    /// </summary>
    [JsonProperty("providerId")]
    public long ProviderId { get; set; }

    /// <summary xml:lang = "ru">
    /// Внешний идентификатор продукта.
    /// </summary>
    [JsonProperty("externalId")]
    public long ExternalId { get; set; }
}
