namespace MobileClient.Logic.Transport;
public interface IHttpClientFacade
{
    public Task<HttpResponseMessage> GetAsync(string url, HttpContent content = null!);

    public Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
}
