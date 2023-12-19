using Newtonsoft.Json;

namespace MobileClient.Logic.Transport;

public sealed class HttpResponseDeserializer<TSource, TTarget> : IDeserializer<HttpResponseMessage, TTarget>
{
    public TTarget Deserialize(HttpResponseMessage source)
    {
        var stringContent = source.Content.ReadAsStringAsync().Result;

        return JsonConvert.DeserializeObject<TTarget>(stringContent)!;
    }
}
