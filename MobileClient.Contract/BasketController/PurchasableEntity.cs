using Newtonsoft.Json;

namespace MobileClient.Contract.BasketController;
public sealed class PurchasableEntity
{
    /// <summary  xml:lang = "ru">
    ///  Продукт.
    /// </summary>
    [JsonProperty("product")]
    public Product Product { get; set; }

    /// <summary  xml:lang = "ru">
    /// Количество продуктов.
    /// </summary>
    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    /// <summary xml:lang = "ru">
    /// Выбран ли продукт.
    /// </summary>
    [JsonProperty("selected")]
    public bool Selected { get; set; }

    [JsonProperty("isApproved")]
    public bool? IsApproved { get; set; }

    public decimal SumCost => Quantity * Product.FinalCost;
}
