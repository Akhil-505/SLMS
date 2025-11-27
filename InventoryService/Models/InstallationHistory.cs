namespace InventoryService.Models
{
    public class InstallationHistory
    {
        public int Id { get; set; }
        public int InstalledSoftwareId { get; set; }

        public InstalledSoftware? InstalledSoftware { get; set; }

        public string Action { get; set; } = ""; // Installed/Uninstalled/Updated
        public string PerformedBy { get; set; } = "";
        public string Notes { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
