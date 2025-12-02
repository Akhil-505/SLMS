using MailService.Models;
using MailService.Repositories;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MailService.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IEmailRepository _repository;

        public EmailService(IConfiguration config, IEmailRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public async Task<bool> SendEmailAsync(EmailRequest request)
        {
            var smtpCfg = _config.GetSection("Smtp");
            bool success = true;

            var message = new MimeMessage();

            // FIXED: Use Parse()
            message.From.Add(MailboxAddress.Parse(smtpCfg["From"]));
            message.To.Add(MailboxAddress.Parse(request.To));

            message.Subject = request.Subject;
            message.Body = new TextPart("plain") { Text = request.Body };

            try
            {
                using var client = new MailKit.Net.Smtp.SmtpClient();

                await client.ConnectAsync(
                    smtpCfg["Host"],
                    int.Parse(smtpCfg["Port"]),
                    bool.Parse(smtpCfg["UseSsl"])
                );

                if (!string.IsNullOrWhiteSpace(smtpCfg["User"]))
                {
                    await client.AuthenticateAsync(smtpCfg["User"], smtpCfg["Pass"]);
                }

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("EMAIL ERROR: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw; // <--- temporarily rethrow to see actual 500 details
            }


            await _repository.AddLogAsync(new EmailLog
            {
                To = request.To,
                Subject = request.Subject,
                Success = success,
                SentAt = DateTime.UtcNow
            });

            return success;
        }
    }
}
