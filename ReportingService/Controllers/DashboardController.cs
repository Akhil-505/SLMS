using Microsoft.AspNetCore.Mvc;
using ReportingService.Services;

namespace ReportingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboard;

        public DashboardController(IDashboardService dashboard)
        {
            _dashboard = dashboard;
        }

        // ---------------------------------------------------------
        // 1. Dashboard Report (Frontend main API)
        // ---------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _dashboard.GetDashboardAsync();
            return Ok(result);
        }
    }
}

