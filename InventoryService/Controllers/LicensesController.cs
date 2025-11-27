using InventoryService.Models;
using InventoryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicensesController : ControllerBase
    {
        private readonly ILicenseService _service;

        public LicensesController(ILicenseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var license = await _service.GetByIdAsync(id);
            return license == null ? NotFound() : Ok(license);
        }

        [HttpPost]
        public async Task<IActionResult> Create(License license)
        {
            var created = await _service.CreateAsync(license);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, License update)
        {
            try
            {
                return Ok(await _service.UpdateAsync(id, update));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? product, [FromQuery] string? vendor)
        {
            return Ok(await _service.SearchAsync(product, vendor));
        }
    }
}
