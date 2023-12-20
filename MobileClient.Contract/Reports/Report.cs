namespace MobileClient.Contract.Reports;
public sealed class Report
{
    /// <summary>
    /// Поставщик по которому составляется отчет.
    /// </summary>
    public Provider Provider { get; }

    /// <summary>
    /// Кол-во проданных продуктов.
    /// </summary>
    public long SoldProductsCount { get; }

    /// <summary>
    /// Самый продоваемый продукт.
    /// </summary>
    public Product ProductMVP { get; }

    /// <summary>
    /// Общая прибыль.
    /// </summary>
    public decimal TotalProfit { get; }
}
