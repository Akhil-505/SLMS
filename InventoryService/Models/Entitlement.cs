namespace InventoryService.Models
{
    public class Entitlement
    {
        public int Id { get; set; }

        public int LicenseId { get; set; }
        public License? License { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public int? DeviceId { get; set; }
        public Device? Device { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
    }
}
