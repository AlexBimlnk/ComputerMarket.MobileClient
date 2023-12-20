using System.Text;

using Microsoft.Extensions.Options;

using MobileClient.Contract.Reports;
using MobileClient.Logic.Configuration;
using MobileClient.Logic.Transport;

namespace MobileClient.Logic.Reports;
public sealed class ReportsAccessor : IReportsAccessor
{
    private readonly IHttpClientFacade _httpClientFacade;
    private readonly ServiceConfig _serviceConfig;
    private readonly IDeserializer<HttpResponseMessage, Report> _reportDeserializer;
    private readonly ISerializer<ReportRequest, string> _reportSerializer;

    public ReportsAccessor(
        IHttpClientFacade httpClientFacade,
        IDeserializer<HttpResponseMessage, Report> reportDeserializer,
        ISerializer<ReportRequest, string> reportSerializer,
        IOptions<ServiceConfig> options)
    {
        _httpClientFacade = httpClientFacade ?? throw new ArgumentNullException(nameof(httpClientFacade));
        _serviceConfig = options.Value ?? throw new ArgumentNullException(nameof(options));
        _reportDeserializer = reportDeserializer ?? throw new ArgumentNullException(nameof(reportDeserializer));
        _reportSerializer = reportSerializer ?? throw new ArgumentNullException(nameof(reportSerializer));
    }

    public async Task CancelOrderByIdAsync(long id)
    {
        var result = await _httpClientFacade.GetAsync(
            $"{_serviceConfig.MarketService}/orders/cancel/{id}");

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();
    }

    public async Task<Report> CreateReportAsync(ReportRequest reportRequest)
    {
        ArgumentNullException.ThrowIfNull(reportRequest);

        var result = await _httpClientFacade.PostAsync(
            $"{_serviceConfig.MarketService}/report/api/create",
            new StringContent(_reportSerializer.Serialize(reportRequest), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
            throw new InvalidOperationException();

        return _reportDeserializer.Deserialize(result);
    }
}
