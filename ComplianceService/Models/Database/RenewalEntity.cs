using System.ComponentModel.DataAnnotations;

namespace ComplianceService.Models.Database
{
    public class RenewalEntity
    {
        [Key]
        public int Id { get; set; }

        public int LicenseId { get; set; }                  // Refers to Inventory Service LicenseId

        public string VendorName { get; set; } = "";
        public string ContractNumber { get; set; } = "";

        public DateTime PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public int ReminderDaysBefore { get; set; } = 30;

        public DateTime? LastReminderSentAt { get; set; }

        public string Status { get; set; } = "Active";      // Active, Expired, Renewed

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
