public class InstalledSoftwareModel
{
    public int Id { get; set; }
    public int DeviceId { get; set; }      // MUST stay non-nullable
    public int LicenseId { get; set; }     // MUST stay non-nullable
    public string ProductName { get; set; } = "";
    public string Version { get; set; } = "";
    public DateTime InstallDate { get; set; }
}
