using Newtonsoft.Json;

namespace MobileClient.Logic.Transport;
public sealed class StringSerializer<TSource, TTarget> : ISerializer<TSource, string>
{
    public string Serialize(TSource source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return JsonConvert.SerializeObject(source);
    }
}
