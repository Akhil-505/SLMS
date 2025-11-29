using InventoryService.Models;

namespace InventoryService.Models
{
    public class EntitlementDto
    {
        public int EntitlementId { get; set; }
        public int LicenseId { get; set; }
        public string ProductName { get; set; } = "";
        public string Vendor { get; set; } = "";
        public string? UserId { get; set; }
        public string? UserIdentifier { get; set; }
        public int? DeviceId { get; set; }
        public string? DeviceIdentifier { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string Notes { get; set; } = "";

        public EntitlementDto(Entitlement e)
        {
            EntitlementId = e.Id;
            LicenseId = e.LicenseId;
            ProductName = e.License?.ProductName ?? "";
            Vendor = e.License?.Vendor ?? "";
            UserId = e.UserId;
            UserIdentifier = e.User?.UserId;
            DeviceId = e.DeviceId;
            DeviceIdentifier = e.Device?.DeviceId;
            AssignedAt = e.AssignedAt;
            ExpiresAt = e.ExpiresAt;
        }
    }
}
