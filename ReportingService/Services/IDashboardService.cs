using ReportingService.Models;

namespace ReportingService.Services
{
    public interface IDashboardService
    {
        Task<DashboardReport> GetDashboardAsync();
    }
}
