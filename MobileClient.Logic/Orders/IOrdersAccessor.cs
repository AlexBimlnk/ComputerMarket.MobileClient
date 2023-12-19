using MobileClient.Contract.Orders;

namespace MobileClient.Logic.Orders;
public interface IOrdersAccessor
{
    Task<IReadOnlyCollection<Order>> GetOrdersAsync();

    Task<Order> GetOrderByIdAsync(long id);

    Task CancelOrderByIdAsync(long id);

    Task PayOrderAsync(long id, OrderPay orderPay);

    Task<IReadOnlyCollection<Order>> GetOrdersForAproveAsync();

    Task ReadyOrder(long id);

    Task ReceiveOrder(long id);
}
