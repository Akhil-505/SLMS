using InventoryService.Models;
using InventoryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstallationsController : ControllerBase
    {
        private readonly IInstallationService _service;

        public InstallationsController(IInstallationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost("install")]
        public async Task<IActionResult> Install(InstalledSoftware install, [FromQuery] string performedBy)
        {
            var created = await _service.InstallAsync(install, performedBy);
            return Ok(created);
        }

        [HttpDelete("{id}/uninstall")]
        public async Task<IActionResult> Uninstall(int id, [FromQuery] string performedBy)
        {
            var ok = await _service.UninstallAsync(id, performedBy);
            return ok ? NoContent() : NotFound();
        }
    }
}
