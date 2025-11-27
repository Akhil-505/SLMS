using ComplianceService.Models.Database;
using ComplianceService.Repositories.Database;
using Microsoft.AspNetCore.Mvc;

namespace ComplianceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IComplianceEventRepository _repo;

        public EventsController(IComplianceEventRepository repo)
        {
            _repo = repo;
        }

        // -----------------------------------------------------------
        // 1. Get all events
        // -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        // -----------------------------------------------------------
        // 2. Get unresolved events only
        // -----------------------------------------------------------
        [HttpGet("unresolved")]
        public async Task<IActionResult> GetUnresolved()
        {
            return Ok(await _repo.GetUnresolvedAsync());
        }

        // -----------------------------------------------------------
        // 3. Resolve event
        // -----------------------------------------------------------
        [HttpPut("{id}/resolve")]
        public async Task<IActionResult> ResolveEvent(int id, [FromBody] string resolutionNote)
        {
            var ev = await _repo.GetByIdAsync(id);
            if (ev == null) return NotFound("Event not found.");

            ev.Resolved = true;
            ev.ResolutionNote = resolutionNote;
            ev.ResolvedBy = "Admin";   // later -> use JWT user
            ev.ResolvedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(ev);
            return Ok(ev);
        }
    }
}
