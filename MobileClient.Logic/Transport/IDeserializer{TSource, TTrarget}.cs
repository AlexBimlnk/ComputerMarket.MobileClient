namespace MobileClient.Logic.Transport;

public interface IDeserializer<TSource, TTrarget>
{
    public TTrarget Deserialize(TSource source);
}
