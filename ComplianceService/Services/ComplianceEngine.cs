using ComplianceService.Models;
using ComplianceService.Models.Database;
using ComplianceService.Repositories.Database;
using ComplianceService.Repositories.Inventory;
using System.Text.Json;

namespace ComplianceService.Services
{
    public class ComplianceEngine : IComplianceEngine
    {
        private readonly ILicenseDataRepository _licenseRepo;
        private readonly IEntitlementDataRepository _entRepo;
        private readonly IInstallationDataRepository _installRepo;
        private readonly ICatalogDataRepository _catalogRepo;

        private readonly IComplianceRuleRepository _ruleRepo;
        private readonly IComplianceEventRepository _eventRepo;
        private readonly INotificationSender _notifier;

        public ComplianceEngine(
            ILicenseDataRepository licenseRepo,
            IEntitlementDataRepository entRepo,
            IInstallationDataRepository installRepo,
            ICatalogDataRepository catalogRepo,
            IComplianceRuleRepository ruleRepo,
            IComplianceEventRepository eventRepo,
            INotificationSender notifier)
        {
            _licenseRepo = licenseRepo;
            _entRepo = entRepo;
            _installRepo = installRepo;
            _catalogRepo = catalogRepo;
            _ruleRepo = ruleRepo;
            _eventRepo = eventRepo;
            _notifier = notifier;
        }

        // -----------------------------------------------------------
        // 1. RUN FULL LICENSE COMPLIANCE CHECKS
        // -----------------------------------------------------------
        public async Task<List<ComplianceResult>> RunChecksAsync()
        {
            var licenses = await _licenseRepo.GetAllLicensesAsync();
            var entitlements = await _entRepo.GetAllEntitlementsAsync();
            var installs = await _installRepo.GetAllInstallationsAsync();
            var rules = await _ruleRepo.GetAllAsync();

            double underuseThreshold = rules
                .Where(r => r.RuleType == "UnderuseThreshold" && r.Enabled)
                .Select(r => double.Parse(r.Value))
                .FirstOrDefault();

            if (underuseThreshold <= 0)
                underuseThreshold = 25; // default 25%

            var results = new List<ComplianceResult>();

            foreach (var lic in licenses)
            {
                int assigned = entitlements.Count(e => e.LicenseId == lic.Id);
                int installed = installs.Count(i => i.LicenseId == lic.Id);

                int usage = Math.Max(assigned, installed);
                int total = lic.TotalEntitlements == 0 ? 1 : lic.TotalEntitlements;

                double usagePct = (double)usage / total * 100;

                bool overUsed = usage > total;
                bool underUsed = usagePct < underuseThreshold;

                string message =
                    overUsed ? "Overused" :
                    underUsed ? $"Underused ({usagePct:0.##}% < {underuseThreshold}%)" :
                    "Compliant";

                results.Add(new ComplianceResult
                {
                    LicenseId = lic.Id,
                    LicenseName = lic.ProductName,
                    Vendor = lic.Vendor,
                    TotalEntitlements = total,
                    AssignedEntitlements = assigned,
                    TotalInstallations = installed,
                    IsCompliant = !overUsed,
                    Message = message
                });
            }

            return results;
        }

        // -----------------------------------------------------------
        // 2. EXPIRY CHECKS
        // -----------------------------------------------------------
        public async Task<List<ExpiryAlert>> CheckExpiriesAsync()
        {
            var licenses = await _licenseRepo.GetAllLicensesAsync();
            var rules = await _ruleRepo.GetAllAsync();

            int expiryWindow = rules
                .Where(r => r.RuleType == "ExpiryWindowDays" && r.Enabled)
                .Select(r => int.Parse(r.Value))
                .FirstOrDefault();

            if (expiryWindow <= 0)
                expiryWindow = 30;

            DateTime cutoff = DateTime.UtcNow.AddDays(expiryWindow);

            var alerts = new List<ExpiryAlert>();

            foreach (var lic in licenses)
            {
                DateTime? expiry =
                    lic.ExpiryDate ??
                    lic.VendorContract?.ExpiryDate;

                if (expiry != null && expiry <= cutoff)
                {
                    alerts.Add(new ExpiryAlert
                    {
                        LicenseId = lic.Id,
                        LicenseName = lic.ProductName,
                        Vendor = lic.Vendor,
                        ExpiryDate = expiry.Value,
                        Message = $"License expires on {expiry:yyyy-MM-dd}"
                    });
                }
            }

            return alerts;
        }

        // -----------------------------------------------------------
        // 3. UNAUTHORIZED SOFTWARE CHECKS
        // -----------------------------------------------------------
        public async Task<List<UnauthorizedInstall>> CheckUnauthorizedAsync()
        {
            var installs = await _installRepo.GetAllInstallationsAsync();
            var catalog = await _catalogRepo.GetCatalogAsync();

            var list = new List<UnauthorizedInstall>();

            foreach (var ins in installs)
            {
                bool exists = catalog.Any(c =>
                    c.ProductName.Equals(ins.ProductName, StringComparison.OrdinalIgnoreCase));

                if (!exists)
                {
                    list.Add(new UnauthorizedInstall
                    {
                        DeviceId = ins.DeviceId,
                        ProductName = ins.ProductName,
                        Version = ins.Version,
                        Message = "Software not found in catalog."
                    });
                }

                if (ins.LicenseId == null || ins.LicenseId == 0)
                {
                    list.Add(new UnauthorizedInstall
                    {
                        DeviceId = ins.DeviceId,
                        ProductName = ins.ProductName,
                        Version = ins.Version,
                        Message = "Installed without valid license."
                    });
                }
            }

            return list;
        }

        // -----------------------------------------------------------
        // 4. COMBINED REPORT
        // -----------------------------------------------------------
        public async Task<ComplianceReport> GenerateFullReportAsync()
        {
            return new ComplianceReport
            {
                LicenseCompliance = await RunChecksAsync(),
                ExpiringLicenses = await CheckExpiriesAsync(),
                UnauthorizedInstalls = await CheckUnauthorizedAsync()
            };
        }

        // -----------------------------------------------------------
        // 5. PERSIST NON-COMPLIANT EVENTS
        // -----------------------------------------------------------
        public async Task PersistFindingsAsync(List<ComplianceResult> results)
        {
            foreach (var r in results.Where(x => !x.IsCompliant))
            {
                var entity = new ComplianceEventEntity
                {
                    EventType = r.Message.Contains("Overused") ? "Overuse" :
                                r.Message.Contains("Underused") ? "Underuse" :
                                "Unknown",

                    Severity = r.Message.Contains("Overused") ? "High" : "Low",
                    LicenseId = r.LicenseId,
                    CreatedAt = DateTime.UtcNow,
                    Details = r.Message,
                    MetadataJson = JsonSerializer.Serialize(r)
                };

                await _eventRepo.AddAsync(entity);

                await _notifier.SendAsync(new Notification
                {
                    Subject = $"Compliance Alert: {entity.EventType}",
                    Message = r.Message,
                    PayloadJson = entity.MetadataJson
                });
            }
        }
    }
}
