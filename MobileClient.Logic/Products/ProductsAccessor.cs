using Microsoft.Extensions.Options;

using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic.Products;
public sealed class ProductsAccessor : IProductsAccessor
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;

    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<ItemType>> _categoriesDeserializer;
    private readonly IDeserializer<HttpResponseMessage, Catalog> _catalogDeserializer;
    private readonly IDeserializer<HttpResponseMessage, Product> _productDeserializer;

    public ProductsAccessor(
        IHttpClientFacade httpClientFacade,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<ItemType>> categoriesDeserializer,
        IDeserializer<HttpResponseMessage, Catalog> catalogDeserializer,
        IDeserializer<HttpResponseMessage, Product> productDeserializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));

        _categoriesDeserializer = categoriesDeserializer ?? throw new ArgumentNullException(nameof(categoriesDeserializer));
        _catalogDeserializer = catalogDeserializer ?? throw new ArgumentNullException(nameof(catalogDeserializer));
        _productDeserializer = productDeserializer ?? throw new ArgumentNullException(nameof(productDeserializer));
    }

    public async Task<Catalog> GetCatalogAsync(Catalog catalog)
    {
        ArgumentNullException.ThrowIfNull(catalog);

        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/products/api/catalog" +
                $"?typeId={catalog.TypeId}&selected{catalog.Params}&searchString={catalog.SearchString}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _catalogDeserializer.Deserialize(result);
    }

    public async Task<IReadOnlyCollection<ItemType>> GetCategoriesAsync()
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/products/api/categories");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _categoriesDeserializer.Deserialize(result);
    }

    public async Task<Product> GetProductAsync(long itemId, long providerId)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/products/api/product?itemId={itemId}&providerId={providerId}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _productDeserializer.Deserialize(result);
    }
}
