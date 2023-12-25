using MobileClient.Contract;
using MobileClient.Contract.BasketController;

namespace MobileClient.Logic.Basket;
public interface IBasketAccessor
{
    public Task<IReadOnlyCollection<PurchasableEntity>> GetPurchasableEntitiesAsync();

    public Task AddOrIncreaseToBasketAsync(long providerId, long itemId);

    public Task DeleteFromBasketAsync(long providerId, long itemId);

    public Task DecreaseInBasketAsync(long providerId, long itemId);

    public Task CreateOrderAsync(IReadOnlySet<(ID, ID)> toOrder);
}
