namespace ComplianceService.Models
{
    public class UnauthorizedInstall
    {
        public string DeviceId { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string Version { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
