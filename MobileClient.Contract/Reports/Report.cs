using Newtonsoft.Json;

namespace MobileClient.Contract.Reports;
public sealed class Report
{
    /// <summary>
    /// Поставщик по которому составляется отчет.
    /// </summary>
    [JsonProperty("provider")]
    public Provider Provider { get; set; }

    /// <summary>
    /// Кол-во проданных продуктов.
    /// </summary>
    [JsonProperty("soldProductsCount")]
    public long SoldProductsCount { get; set; }

    /// <summary>
    /// Самый продоваемый продукт.
    /// </summary>
    [JsonProperty("productMVP")]
    public Product ProductMVP { get; set; }

    /// <summary>
    /// Общая прибыль.
    /// </summary>
    [JsonProperty("totalProfit")]
    public decimal TotalProfit { get; set; }
}
