using ComplianceService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComplianceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplianceController : ControllerBase
    {
        private readonly IComplianceEngine _engine;

        public ComplianceController(IComplianceEngine engine)
        {
            _engine = engine;
        }

        // -----------------------------------------------------------
        // 1. Get FULL compliance report (no DB persistence)
        // -----------------------------------------------------------
        [HttpGet("report")]
        public async Task<IActionResult> GetReport()
        {
            var report = await _engine.GenerateFullReportAsync();
            return Ok(report);
        }

        // -----------------------------------------------------------
        // 2. Run compliance checks and PERSIST events
        // -----------------------------------------------------------
        [HttpPost("run")]
        public async Task<IActionResult> RunComplianceEngine()
        {
            var results = await _engine.RunChecksAsync();
            await _engine.PersistFindingsAsync(results);

            return Ok(new
            {
                message = "Compliance checks executed successfully.",
                totalNonCompliant = results.Count(x => !x.IsCompliant)
            });
        }
    }
}
