using System.Net;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MobileClient.Logic.Account;
using MobileClient.Logic.Basket;
using MobileClient.Logic.Builder;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Links;
using MobileClient.Logic.Orders;
using MobileClient.Logic.Products;
using MobileClient.Logic.Providers;
using MobileClient.Logic.Reports;
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
            .AddSingleton<IBasketAccessor, BasketAccessor>()
            .AddSingleton<IOrdersAccessor, OrdersAccessor>()
            .AddSingleton<ILinksAccessor, LinksAccessor>()
            .AddSingleton<IReportsAccessor, ReportsAccessor>()
            .AddSingleton<IProviderAccessor, ProviderAccessor>()
            .AddSingleton<IProductsAccessor, ProductsAccessor>()
            .AddSingleton<IBuilderAccessor, BuilderAccessor>();

    private static IServiceCollection AddTransport(this IServiceCollection services)
        => services
            .AddHttp();

    private static IServiceCollection AddHttp(this IServiceCollection services)
        => services
            .AddHttpClient("myClient")
            .ConfigureHttpClient((client) =>
            {
                client.MaxResponseContentBufferSize = 1024 * 1024 * 1024;
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.DefaultRequestHeaders.ConnectionClose = true;
                client.Timeout = TimeSpan.FromSeconds(5);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
                UseProxy = false,
                UseCookies = true,
                ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true,
                CookieContainer = new System.Net.CookieContainer()
            })
            .Services
            .AddSingleton<HttpClient>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                return factory.CreateClient("myClient");
            })
            .AddSingleton<IHttpClientFacade, HttpClientFacade>();

    private static IServiceCollection AddSerialization(this IServiceCollection services)
        => services
            .AddSingleton(typeof(ISerializer<,>), typeof(StringSerializer<,>))
            .AddSingleton(typeof(IDeserializer<,>), typeof(HttpResponseDeserializer<,>));

    private static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<ServiceConfig>(configuration.GetSection(nameof(ServiceConfig)));
}
