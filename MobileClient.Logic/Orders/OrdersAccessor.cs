using System.Text;

using Microsoft.Extensions.Options;

using MobileClient.Contract.Orders;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic.Orders;
public sealed class OrdersAccessor : IOrdersAccessor
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;
    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<Order>> _ordersDeserializer;
    private readonly IDeserializer<HttpResponseMessage, Order> _orderDeserializer;
    private readonly ISerializer<OrderPay, string> _paySerializer;

    public OrdersAccessor(
        IHttpClientFacade httpClientFacade,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<Order>> ordersDeserializer,
        IDeserializer<HttpResponseMessage, Order> orderDeserializer,
        ISerializer<OrderPay, string> paySerializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));
        _ordersDeserializer = ordersDeserializer ?? throw new ArgumentNullException(nameof(ordersDeserializer));
        _orderDeserializer = orderDeserializer ?? throw new ArgumentNullException(nameof(orderDeserializer));
        _paySerializer = paySerializer ?? throw new ArgumentNullException(nameof(paySerializer));
    }

    public async Task CancelOrderByIdAsync(long id)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/orders/cancel/{id}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task<Order> GetOrderByIdAsync(long id)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/orders/api/details/{id}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _orderDeserializer.Deserialize(result);
    }

    public async Task<IReadOnlyCollection<Order>> GetOrdersAsync()
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/orders/api/list");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _ordersDeserializer.Deserialize(result);
    }

    public async Task<IReadOnlyCollection<Order>> GetOrdersForAproveAsync()
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/orders/api/aprove");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _ordersDeserializer.Deserialize(result);
    }

    public async Task PayOrderAsync(long id, OrderPay orderPay)
    {
        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/orders/api/pay/{id}",
            new StringContent(_paySerializer.Serialize(orderPay), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task ReadyOrder(long id)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/orders/ready/{id}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task ReceiveOrder(long id)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/orders/receive/{id}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }
}
