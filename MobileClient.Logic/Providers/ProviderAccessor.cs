using System.Text;

using Microsoft.Extensions.Options;

using MobileClient.Contract;
using MobileClient.Contract.BasketController;
using MobileClient.Contract.Orders;
using MobileClient.Contract.Providers;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic.Providers;
public sealed class ProviderAccessor : IProviderAccessor
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;

    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<Provider>> _providersDeserializer;
    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<User>> _agentsDeserializer;
    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<Order>> _ordersDeserializer;
    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<PurchasableEntity>> _orderDetailDeserializer;

    private readonly ISerializer<ProviderRegister, string> _registerSerializer;
    private readonly ISerializer<EditProvider, string> _editProviderSerializer;
    private readonly ISerializer<NewAgent, string> _agentSerializer;

    public ProviderAccessor(
        IHttpClientFacade httpClientFacade,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<Provider>> providersDeserializer,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<User>> agentsDeserializer,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<Order>> ordersDeserializer,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<PurchasableEntity>> orderDetailDeserializer,
        ISerializer<ProviderRegister, string> registerSerializer,
        ISerializer<EditProvider, string> editProviderSerializer,
        ISerializer<NewAgent, string> agentSerializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));

        _providersDeserializer = providersDeserializer ?? throw new ArgumentNullException(nameof(providersDeserializer));
        _agentsDeserializer = agentsDeserializer ?? throw new ArgumentNullException(nameof(agentsDeserializer));
        _ordersDeserializer = ordersDeserializer ?? throw new ArgumentNullException(nameof(ordersDeserializer));
        _orderDetailDeserializer = orderDetailDeserializer ?? throw new ArgumentNullException(nameof(orderDetailDeserializer));

        _registerSerializer = registerSerializer ?? throw new ArgumentNullException(nameof(registerSerializer));
        _editProviderSerializer = editProviderSerializer ?? throw new ArgumentNullException(nameof(editProviderSerializer));
        _agentSerializer = agentSerializer ?? throw new ArgumentNullException(nameof(agentSerializer));
    }

    public async Task<IReadOnlyCollection<User>> AddAgentsByProviderIdAsync(long providerId, NewAgent agent)
    {
        ArgumentNullException.ThrowIfNull(agent);

        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/provider/api/agents/{providerId}/add",
            new StringContent(_agentSerializer.Serialize(agent), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _agentsDeserializer.Deserialize(result);
    }

    public async Task AproveProviderAsync(EditProvider aproveProvider)
    {
        ArgumentNullException.ThrowIfNull(aproveProvider);

        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/provider/api/aprove",
            new StringContent(_editProviderSerializer.Serialize(aproveProvider), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task EditProviderAssync(EditProvider editProvider)
    {
        ArgumentNullException.ThrowIfNull(editProvider);

        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/provider/api/edit",
            new StringContent(_editProviderSerializer.Serialize(editProvider), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task<IReadOnlyCollection<User>> GetAgentsByProviderIdAsync(long providerId)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/provider/api/agents/{providerId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _agentsDeserializer.Deserialize(result);
    }

    public async Task<IReadOnlyCollection<PurchasableEntity>> GetDetailsOrderRelatedWithAuthProviderByIdAsync(long orderRelatedWithProviderId)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/provider/api/orders/details/{orderRelatedWithProviderId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _orderDetailDeserializer.Deserialize(result);
    }

    public async Task<IReadOnlyCollection<Order>> GetOrdersRelatedWithAuthProviderAsync()
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/provider/api/orders");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _ordersDeserializer.Deserialize(result);
    }

    public async Task<IReadOnlyCollection<Provider>> GetProvidersAsync()
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/provider/api/list");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _providersDeserializer.Deserialize(result);
    }

    public async Task<IReadOnlyCollection<Order>> ReadyOrderRelatedWithAuthProviderByIdAsync(long orderRelatedWithProviderId)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/provider/api/ready/{orderRelatedWithProviderId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _ordersDeserializer.Deserialize(result);
    }

    public async Task<IReadOnlyCollection<Order>> DeclineOrderRelatedWithAuthProviderByIdAsync(long orderRelatedWithProviderId)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/provider/api/decline/{orderRelatedWithProviderId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _ordersDeserializer.Deserialize(result);
    }

    public async Task RegisterAsync(ProviderRegister providerRegister)
    {
        ArgumentNullException.ThrowIfNull(providerRegister);

        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/provider/api/register",
            new StringContent(_registerSerializer.Serialize(providerRegister), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task<IReadOnlyCollection<User>> RemoveAgentByProviderIdAsync(long providerId, long agentId)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/provider/api/agents/{providerId}/remove/{agentId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _agentsDeserializer.Deserialize(result);
    }
}
