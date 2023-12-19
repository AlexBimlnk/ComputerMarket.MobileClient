using System.Text;

using Microsoft.Extensions.Options;

using MobileClient.Contract.Links;
using MobileClient.Contract.Orders;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic.Orders;
public sealed class LinksAccessor : ILinksAccessor
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;
    private readonly IDeserializer<HttpResponseMessage, IReadOnlyCollection<Link>> _linksDeserializer;
    private readonly ISerializer<CreateLink, string> _linkSerializer;

    public LinksAccessor(
        IHttpClientFacade httpClientFacade,
        IDeserializer<HttpResponseMessage, IReadOnlyCollection<Link>> linksDeserializer,
        IDeserializer<HttpResponseMessage, Order> orderDeserializer,
        ISerializer<CreateLink, string> linkSerializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));
        _linksDeserializer = linksDeserializer ?? throw new ArgumentNullException(nameof(linksDeserializer));
        _linkSerializer = linkSerializer ?? throw new ArgumentNullException(nameof(linkSerializer));
    }

    public async Task CreateLinkAsync(CreateLink createLink)
    {
        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/links/api/create",
            new StringContent(_linkSerializer.Serialize(createLink), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task DeleteLinkAsync(CreateLink createLink)
    {
        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/links/api/delete",
            new StringContent(_linkSerializer.Serialize(createLink), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task<IReadOnlyCollection<Link>> GetLinksAsync()
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/links/api/list");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _linksDeserializer.Deserialize(result);
    }

}
