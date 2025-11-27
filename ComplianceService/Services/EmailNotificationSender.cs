using System.Threading.Tasks;

namespace ComplianceService.Services
{
    public class EmailNotificationSender : INotificationSender
    {
        public Task SendAsync(Notification msg)
        {
            // Replace with real mail integration (SMTP, SendGrid)
            Console.WriteLine($"[EMAIL] {msg.Subject} - {msg.Message}");
            return Task.CompletedTask;
        }
    }
}
