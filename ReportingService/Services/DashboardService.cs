using ReportingService.Models;

namespace ReportingService.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IReportingService _reporting;

        public DashboardService(IReportingService reporting)
        {
            _reporting = reporting;
        }

        public Task<DashboardReport> GetDashboardAsync()
        {
            return _reporting.GetDashboardReportAsync();
        }
    }
}
