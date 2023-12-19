using MobileClient.Contract.BasketController;

using Newtonsoft.Json;

namespace MobileClient.Contract.Orders;
public sealed class Order
{
    /// <summary xml:lang = "ru">
    /// Позиции в заказе.
    /// </summary>
    [JsonProperty("items")]
    public IReadOnlyCollection<PurchasableEntity> Items { get; set; }

    /// <summary xml:lang = "ru">
    /// Пользователь создавший заказ.
    /// </summary>
    [JsonProperty("creator")]
    public User Creator { get; set; }

    /// <summary xml:lang = "ru">
    /// Состояние заказа.
    /// </summary>
    [JsonProperty("state")]
    public OrderState State { get; set; }

    /// <summary xml:lang = "ru">
    /// Дата создания заказа.
    /// </summary>
    [JsonProperty("orderDate")]
    public DateTime OrderDate { get; set; }

    /// <inheritdoc/>
    [JsonProperty("key")]
    public ID Key { get; set; }

    /// <summary xml:lang = "ru">
    /// Метод высчитывающий итоговую стоимость заказа.
    /// </summary>
    /// <returns xml:lang = "ru">Итоговая стоимость.</returns>
    public decimal GetSumCost() => Items.Sum(x => x.Product.FinalCost * x.Quantity);
}
