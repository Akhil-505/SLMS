using ComplianceService.Models;

namespace ComplianceService.Services
{
    public interface IComplianceEngine
    {
        Task<List<ComplianceResult>> RunChecksAsync();
        Task<List<ExpiryAlert>> CheckExpiriesAsync();
        Task<List<UnauthorizedInstall>> CheckUnauthorizedAsync();
        Task<ComplianceReport> GenerateFullReportAsync();
        Task PersistFindingsAsync(List<ComplianceResult> results);
    }
}
