namespace ComplianceService.Models
{
    public class EntitlementModel
    {
        public int Id { get; set; }
        public int LicenseId { get; set; }

        public string? UserId { get; set; }

        public int? DeviceId { get; set; }   // <-- FIX: MUST BE NULLABLE

        public DateTime AssignedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public string Notes { get; set; } = "";
    }
}
