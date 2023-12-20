using Newtonsoft.Json;

namespace MobileClient.Contract.Reports;
public sealed class Report
{
    /// <summary>
    /// Поставщик по которому составляется отчет.
    /// </summary>
    [JsonProperty("provider")]
    public Provider Provider { get; }

    /// <summary>
    /// Кол-во проданных продуктов.
    /// </summary>
    [JsonProperty("soldProductsCount")]
    public long SoldProductsCount { get; }

    /// <summary>
    /// Самый продоваемый продукт.
    /// </summary>
    [JsonProperty("productMVP")]
    public Product ProductMVP { get; }

    /// <summary>
    /// Общая прибыль.
    /// </summary>
    [JsonProperty("totalProfit")]
    public decimal TotalProfit { get; }
}
