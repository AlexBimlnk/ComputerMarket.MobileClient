using Microsoft.Extensions.Configuration;

using MobileClient.Logic;
using MobileClient.Logic.Configuration;

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
            .AddSingleton<ILoginHandler, LoginHandler>();

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
            .AddSingleton(typeof(ISerializer<,>), typeof(StringSerializer<,>));
        //.AddSingleton<IDeserializer<HttpResponseMessage>, HttpClientFacade>();

    private static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<ServiceConfig>(configuration.GetSection(nameof(ServiceConfig)));
}
