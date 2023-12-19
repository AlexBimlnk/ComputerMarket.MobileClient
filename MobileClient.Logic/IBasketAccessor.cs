using MobileClient.Contract.BasketController;

namespace MobileClient.Logic;
public interface IBasketAccessor
{
    public Task<IReadOnlyCollection<PurchasableEntity>> GetPurchasableEntitiesAsync();

    public Task AddOrIncreaseToBasketAsync(long providerId, long itemId);

    public Task DeleteFromBasketAsync(long providerId, long itemId);

    public Task DecreaseInBasketAsync(long providerId, long itemId);
}
