using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MobileClient.Logic;

namespace MobileClient.UI;

public static class Registration
{
    public static IServiceCollection AddMyServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddHttp();

    private static IServiceCollection AddHttp(this IServiceCollection services)
        => services
            .AddHttpClient("myClient")
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                UseCookies = true,
                ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true,
                CookieContainer = new System.Net.CookieContainer()
            })
            .Services
            .AddSingleton<IHttpClientFacade, HttpClientFacade>();

    private static IServiceCollection AddSerialization(this IServiceCollection services)
        => services;
            //.AddSingleton<IDeserializer<HttpResponseMessage>, HttpClientFacade>();
}
