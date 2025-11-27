namespace ComplianceService.Services
{
    public interface INotificationSender
    {
        Task SendAsync(Notification msg);
    }

    public class Notification
    {
        public string Subject { get; set; } = "";
        public string Message { get; set; } = "";
        public string? PayloadJson { get; set; }
    }
}
