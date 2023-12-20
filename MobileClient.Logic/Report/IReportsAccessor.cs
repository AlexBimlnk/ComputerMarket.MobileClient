using MobileClient.Contract.Reports;

namespace MobileClient.Logic.Reports;
public interface IReportsAccessor
{
    Task<Report> CreateReportAsync(ReportRequest reportRequest);
}
