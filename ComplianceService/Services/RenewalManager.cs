using ComplianceService.Models.Database;
using ComplianceService.Repositories.Database;
using System.Text.Json;

namespace ComplianceService.Services
{
    public class RenewalManager : IRenewalManager
    {
        private readonly IRenewalRepository _renewalRepo;
        private readonly INotificationSender _notifier;

        public RenewalManager(IRenewalRepository renewalRepo, INotificationSender notifier)
        {
            _renewalRepo = renewalRepo;
            _notifier = notifier;
        }

        public async Task<List<RenewalEntity>> GetExpiringAsync(int days)
        {
            var all = await _renewalRepo.GetAllAsync();
            var cutoff = DateTime.UtcNow.AddDays(days);

            return all.Where(r => r.ExpiryDate != null &&
                                  r.ExpiryDate <= cutoff &&
                                  r.Status == "Active")
                      .ToList();
        }

        public async Task SendRemindersAsync(int days)
        {
            var expiring = await GetExpiringAsync(days);

            foreach (var r in expiring)
            {
                await _notifier.SendAsync(new Notification
                {
                    Subject = $"Renewal Reminder: {r.VendorName}",
                    Message = $"Contract {r.ContractNumber} expires on {r.ExpiryDate:yyyy-MM-dd}",
                    PayloadJson = JsonSerializer.Serialize(r)
                });

                r.LastReminderSentAt = DateTime.UtcNow;
                await _renewalRepo.UpdateAsync(r);
            }
        }

        public async Task MarkRenewedAsync(int id, string note)
        {
            var r = await _renewalRepo.GetByIdAsync(id);
            if (r == null) throw new Exception("Renewal record not found");

            r.Status = "Renewed";
            r.Notes = note;

            await _renewalRepo.UpdateAsync(r);
        }
    }
}
