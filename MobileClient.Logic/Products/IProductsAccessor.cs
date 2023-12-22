using MobileClient.Contract;
using MobileClient.Contract.Products;

namespace MobileClient.Logic.Products;

public interface IProductsAccessor
{
    public Task<IReadOnlyCollection<ItemType>> GetCategoriesAsync();

    public Task<Catalog> GetCatalogAsync(Catalog catalog);

    public Task<Product> GetProductAsync(long itemId, long providerId);
}
