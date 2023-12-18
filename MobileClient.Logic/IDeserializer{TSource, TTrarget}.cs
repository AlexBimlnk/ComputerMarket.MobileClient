namespace MobileClient.Logic;

public interface IDeserializer<TSource, TTrarget>
{
    public TTrarget Deserialize(TSource source);
}
