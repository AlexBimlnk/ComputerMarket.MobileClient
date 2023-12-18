using Newtonsoft.Json;

namespace MobileClient.Logic;

public sealed class HttpResponseDeserializer<TTarget> : IDeserializer<HttpResponseMessage, TTarget>
{
    public TTarget Deserialize(HttpResponseMessage source)
    {
        var stringContent = source.Content.ReadAsStringAsync().Result;

        return JsonConvert.DeserializeObject<TTarget>(stringContent)!;
    }
}
