namespace MobileClient.Logic;
public interface ISerializer<TSource, TTarget>
{
    public TTarget Serialize(TSource source);
}
