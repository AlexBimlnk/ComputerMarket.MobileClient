using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using MobileClient.Contract.AccountController;
using MobileClient.Contract.BasketController;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic;

public sealed class BasketAccessor : IBasketAccessor
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;
    private readonly ISerializer<Login, string> _loginSerializer;
    private readonly ISerializer<Register, string> _registerSerializer;
    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<PurchasableEntity>> _basketDeserializer;

    public BasketAccessor(
        IHttpClientFacade httpClientFacade,
        ISerializer<Login, string> loginSerializer,
        ISerializer<Register, string> registerSerializer,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<PurchasableEntity>> deserializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));
        _loginSerializer = loginSerializer ?? throw new ArgumentNullException(nameof(loginSerializer));
        _registerSerializer = registerSerializer ?? throw new ArgumentNullException(nameof(registerSerializer));
        _basketDeserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
    }

    public async Task AddOrIncreaseToBasketAsync(long providerId, long itemId)
    {
        var result = await _httpClientFacade.GetAsync(
                $"{_serviceConfig.MarketService}/basket/add/?itemId={itemId}&&providerId={providerId}");

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
