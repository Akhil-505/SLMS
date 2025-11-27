using ComplianceService.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ComplianceService.Services
{
    public class ComplianceSchedulerHostedService : BackgroundService
    {
        private readonly ILogger<ComplianceSchedulerHostedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _config;

        public ComplianceSchedulerHostedService(
            ILogger<ComplianceSchedulerHostedService> logger,
            IServiceScopeFactory scopeFactory,
            IConfiguration config)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int complianceIntervalMinutes =
                _config.GetValue<int>("Scheduler:ComplianceRunIntervalMinutes");

            int renewalReminderDays =
                _config.GetValue<int>("Scheduler:RenewalReminderDays");

            if (complianceIntervalMinutes <= 0)
                complianceIntervalMinutes = 60;

            _logger.LogInformation(
                $"Compliance Scheduler started. Running every {complianceIntervalMinutes} minutes.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var complianceEngine = scope.ServiceProvider.GetRequiredService<IComplianceEngine>();
                    var renewalManager = scope.ServiceProvider.GetRequiredService<IRenewalManager>();

                    // ------------------------------------------------------
                    // 1. Run compliance checks
                    // ------------------------------------------------------
                    var results = await complianceEngine.RunChecksAsync();
                    await complianceEngine.PersistFindingsAsync(results);

                    _logger.LogInformation("Compliance checks executed and stored.");

                    // ------------------------------------------------------
                    // 2. Send renewal reminders
                    // ------------------------------------------------------
                    await renewalManager.SendRemindersAsync(renewalReminderDays);

                    _logger.LogInformation("Renewal reminders sent.");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Scheduler error occurred.");
                }

                await Task.Delay(TimeSpan.FromMinutes(complianceIntervalMinutes), stoppingToken);
            }
        }
    }
}
