namespace MobileClient.Logic.Transport;
public interface ISerializer<TSource, TTarget>
{
    public TTarget Serialize(TSource source);
}
