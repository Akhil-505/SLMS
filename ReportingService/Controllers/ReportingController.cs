using Microsoft.AspNetCore.Mvc;
using ReportingService.Services;

namespace ReportingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingService _reporting;

        public ReportingController(IReportingService reporting)
        {
            _reporting = reporting;
        }

        // ---------------------------------------------------------
        // 1. License Usage Summary
        // ---------------------------------------------------------
        [HttpGet("license-usage")]
        public async Task<IActionResult> GetLicenseUsage()
        {
            var result = await _reporting.GetLicenseUsageAsync();
            return Ok(result);
        }

        // ---------------------------------------------------------
        // 2. Compliance Summary
        // ---------------------------------------------------------
        [HttpGet("compliance-summary")]
        public async Task<IActionResult> GetComplianceSummary()
        {
            var result = await _reporting.GetComplianceSummaryAsync();
            return Ok(result);
        }
    }
}
