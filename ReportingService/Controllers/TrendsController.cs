using Microsoft.AspNetCore.Mvc;
using ReportingService.Services;

namespace ReportingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrendsController : ControllerBase
    {
        private readonly ITrendService _trend;

        public TrendsController(ITrendService trend)
        {
            _trend = trend;
        }

        // ---------------------------------------------------------
        // 1. Installation Trend Data (Charts)
        // ---------------------------------------------------------
        [HttpGet("installations")]
        public async Task<IActionResult> GetInstallationTrend()
        {
            var data = await _trend.GetInstallationTrendAsync();
            return Ok(data);
        }

        // ---------------------------------------------------------
        // 2. Compliance Trend Data (Charts)
        // ---------------------------------------------------------
        [HttpGet("compliance")]
        public async Task<IActionResult> GetComplianceTrend()
        {
            var data = await _trend.GetComplianceTrendAsync();
            return Ok(data);
        }
    }
}

