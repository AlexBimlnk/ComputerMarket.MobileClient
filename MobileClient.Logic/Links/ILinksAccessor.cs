using MobileClient.Contract.Links;

namespace MobileClient.Logic.Orders;
public interface ILinksAccessor
{
    Task<IReadOnlyCollection<Link>> GetLinksAsync();

    Task CreateLinkAsync(CreateLink createLink);

    Task DeleteLinkAsync(CreateLink createLink);
}
