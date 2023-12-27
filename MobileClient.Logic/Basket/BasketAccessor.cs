using Microsoft.Extensions.Options;

using MobileClient.Contract;
using MobileClient.Contract.BasketController;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic.Basket;

public sealed class BasketAccessor : IBasketAccessor
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;
    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<PurchasableEntity>> _basketDeserializer;

    public BasketAccessor(
        IHttpClientFacade httpClientFacade,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<PurchasableEntity>> deserializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));
        _basketDeserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
    }

    public async Task AddOrIncreaseToBasketAsync(long providerId, long itemId)
    {
        var result = await _httpClientFacade.GetAsync(
                $"{_serviceConfig.MarketService}/basket/add/?itemId={itemId}&&providerId={providerId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task CreateOrderAsync(IReadOnlySet<(ID itemId, ID providerId)> toOrder)
    {
        ArgumentNullException.ThrowIfNull(toOrder);

        var selectedRequest = string.Join(
            ",",
            toOrder.Select(pair => $"{pair.itemId}_{pair.providerId}"));

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("Selected", selectedRequest),
        });

        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/basket/api/create_order",
            content);

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task DecreaseInBasketAsync(long providerId, long itemId)
    {
        var result = await _httpClientFacade.GetAsync(
                $"{_serviceConfig.MarketService}/basket/remove/?itemId={itemId}&&providerId={providerId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }
    public async Task DeleteFromBasketAsync(long providerId, long itemId)
    {
        var result = await _httpClientFacade.GetAsync(
                $"{_serviceConfig.MarketService}/basket/delete/?itemId={itemId}&&providerId={providerId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }
    public async Task<IReadOnlyCollection<PurchasableEntity>> GetPurchasableEntitiesAsync()
    {
        var result = await _httpClientFacade.GetAsync(
                $"{_serviceConfig.MarketService}/basket/api/index");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _basketDeserializer.Deserialize(result);
    }
}
