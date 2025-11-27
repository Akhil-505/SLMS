namespace InventoryService.Models
{
    public class InstalledSoftware
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }
        public Device? Device { get; set; }

        public int? LicenseId { get; set; }
        public License? License { get; set; }

        public string ProductName { get; set; } = "";
        public string Version { get; set; } = "";
        public DateTime InstallDate { get; set; } = DateTime.UtcNow;

        public List<InstallationHistory> History { get; set; } = new();
    }
}
