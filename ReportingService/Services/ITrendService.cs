using ReportingService.Models;

namespace ReportingService.Services
{
    public interface ITrendService
    {
        Task<List<TrendPoint>> GetInstallationTrendAsync();
        Task<List<TrendPoint>> GetComplianceTrendAsync();
    }
}
