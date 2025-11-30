using ComplianceService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComplianceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplianceController : ControllerBase
    {
        private readonly ComplianceEngineService _engine;

        public ComplianceController(ComplianceEngineService engine)
        {
            _engine = engine;
        }

        [HttpPost("run")]
        public async Task<IActionResult> Run()
        {
            return Ok(await _engine.RunAsync());
        }

        [HttpGet("events")]
        public async Task<IActionResult> Events()
        {
            return Ok(await _engine.GetEventsAsync());
        }
    }
}
