using InventoryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IInstallationHistoryService _service;

        public HistoryController(IInstallationHistoryService service)
        {
            _service = service;
        }

        [HttpGet("{installedSoftwareId}")]
        public async Task<IActionResult> GetHistory(int installedSoftwareId)
        {
            var list = await _service.GetForInstallationAsync(installedSoftwareId);
            return Ok(list);
        }
    }
}

