using InventoryService.Models;
using InventoryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntitlementsController : ControllerBase
    {
        private readonly IEntitlementService _service;

        public EntitlementsController(IEntitlementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ent = await _service.GetByIdAsync(id);
            return ent == null ? NotFound() : Ok(ent);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(Entitlement entitlement)
        {
            try
            {
                var assigned = await _service.AssignAsync(entitlement);
                return Ok(assigned);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
