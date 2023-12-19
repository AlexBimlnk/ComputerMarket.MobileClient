using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MobileClient.Contract.BasketController;
using MobileClient.Logic;
using MobileClient.Logic.Account;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.UI;

public static class Registration
{
    public static IServiceCollection AddMyServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddTransport()
            .AddLogic()
            .AddSerialization()
            .AddConfig(configuration);

    private static IServiceCollection AddLogic(this IServiceCollection services)
        => services
            .AddSingleton<ILoginHandler, LoginHandler>()
            .AddSingleton<IBasketAccessor, BasketAccessor>();

    private static IServiceCollection AddTransport(this IServiceCollection services)
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
        => services
            .AddSingleton(typeof(ISerializer<,>), typeof(StringSerializer<,>))
            .AddSingleton(typeof(IDeserializer<,>), typeof(HttpResponseDeserializer<,>));

    private static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<ServiceConfig>(configuration.GetSection(nameof(ServiceConfig)));
}
