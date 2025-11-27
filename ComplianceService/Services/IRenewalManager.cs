using ComplianceService.Models.Database;

namespace ComplianceService.Services
{
    public interface IRenewalManager
    {
        Task<List<RenewalEntity>> GetExpiringAsync(int days);
        Task SendRemindersAsync(int days);
        Task MarkRenewedAsync(int id, string note);
    }
}
