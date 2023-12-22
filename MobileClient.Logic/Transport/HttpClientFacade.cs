namespace MobileClient.Logic.Transport;
public sealed class HttpClientFacade : IHttpClientFacade
{
    private readonly HttpClient _httpClient;

    public HttpClientFacade(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<HttpResponseMessage> GetAsync(string url, HttpContent content = null)
    {
        if (content == null)
            return await _httpClient.GetAsync(url);
        else
        {
            var st = await content.ReadAsStringAsync();

            using var request = new HttpRequestMessage
            {
                Content = content,
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            return await _httpClient.SendAsync(request)
                .ConfigureAwait(false);
        }
    }

    public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content) =>
         await _httpClient.PostAsync(url, content)
            .ConfigureAwait(false);
}
