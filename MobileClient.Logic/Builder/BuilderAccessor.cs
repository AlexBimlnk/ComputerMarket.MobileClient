using Microsoft.Extensions.Options;

using MobileClient.Contract.Builder;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic.Builder;
public class BuilderAccessor : IBuilderAccessor
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;
    private readonly IDeserializer<HttpResponseMessage, BuildResult> _builderDeserializer;

    public BuilderAccessor(
        IHttpClientFacade httpClientFacade,
        IDeserializer<HttpResponseMessage, BuildResult> deserializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));
        _builderDeserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
    }

    public async Task<BuildResult> GetBuildResultAsync(RequestBuild requestBuild)
    {
        ArgumentNullException.ThrowIfNull(requestBuild);

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("processor", requestBuild.Processor),
            new KeyValuePair<string, string>("motherBoard", requestBuild.MotherBoard)
        });

        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/builder/api/build",
            content);

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _builderDeserializer.Deserialize(result);
    }
}
