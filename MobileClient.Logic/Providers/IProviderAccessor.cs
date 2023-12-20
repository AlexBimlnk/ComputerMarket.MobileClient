using MobileClient.Contract;
using MobileClient.Contract.BasketController;
using MobileClient.Contract.Orders;
using MobileClient.Contract.Providers;

namespace MobileClient.Logic.Providers;
public interface IProviderAccessor
{
    public Task RegisterAsync(ProviderRegister providerRegister);

    public Task<IReadOnlyCollection<Provider>> GetProvidersAsync();

    public Task EditProviderAssync(EditProvider editProvider);

    public Task AproveProviderAsync(EditProvider aproveProvider);

    public Task<IReadOnlyCollection<User>> GetAgentsByProviderIdAsync(long providerId);

    public Task<IReadOnlyCollection<User>> AddAgentsByProviderIdAsync(long providerId, NewAgent agent);

    public Task<IReadOnlyCollection<User>> RemoveAgentByProviderIdAsync(long providerId, long agentId);

    public Task<IReadOnlyCollection<Order>> GetOrdersRelatedWithAuthProviderAsync();

    public Task<IReadOnlyCollection<PurchasableEntity>> GetDetailsOrderRelatedWithAuthProviderByIdAsync(long orderRelatedWithProviderId);

    public Task<IReadOnlyCollection<Order>> ReadyOrderRelatedWithAuthProviderByIdAsync(long orderRelatedWithProviderId);

    public Task<IReadOnlyCollection<Order>> DeclineOrderRelatedWithAuthProviderByIdAsync(long orderRelatedWithProviderId);
}
