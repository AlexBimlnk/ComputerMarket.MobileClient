using MobileClient.Contract.Links;

namespace MobileClient.Logic.Links;
public interface ILinksAccessor
{
    Task<IReadOnlyCollection<Link>> GetLinksAsync();

    Task CreateLinkAsync(CreateLink createLink);

    Task DeleteLinkAsync(CreateLink createLink);
}
