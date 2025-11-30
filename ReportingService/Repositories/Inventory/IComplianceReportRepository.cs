using ReportingService.Models;

namespace ReportingService.Repositories.Compliance
{
    public interface IComplianceReportRepository
    {
        Task<List<ComplianceEventModel>> GetReportAsync();
    }
}
