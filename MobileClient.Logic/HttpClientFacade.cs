﻿namespace MobileClient.Logic;
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
        {
            return await _httpClient.GetAsync(url);
        }
        else
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Content = content;
            return await _httpClient.SendAsync(request);
        }
    }

    public Task<HttpResponseMessage> PostAsync(string url, HttpContent content) =>
        _httpClient.PostAsync(url, content);
}
