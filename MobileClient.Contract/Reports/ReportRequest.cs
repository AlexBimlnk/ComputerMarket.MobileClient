using Newtonsoft.Json;

namespace MobileClient.Contract.Reports;
public sealed class ReportRequest
{
    /// <summary xml:lang = "ru">
    /// Идентификатор поставщика.
    /// </summary>
    [JsonProperty("providerId")]
    public long ProviderId { get; set; }

    /// <summary xml:lang = "ru">
    /// Дата начала периода, по которому будет идти отчет.
    /// </summary>
    [JsonProperty("startPeriod")]
    public DateTime StartPeriod { get; set; }

    /// <summary xml:lang = "ru">
    /// Дата конца периода, по которому будет идти отчет.
    /// </summary>
    [JsonProperty("endPeriod")]
    public DateTime EndPeriod { get; set; }
}
