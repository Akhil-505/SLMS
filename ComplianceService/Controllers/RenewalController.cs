using ComplianceService.Models.Database;
using ComplianceService.Repositories.Database;
using ComplianceService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComplianceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenewalController : ControllerBase
    {
        private readonly IRenewalRepository _repo;
        private readonly IRenewalManager _manager;

        public RenewalController(IRenewalRepository repo, IRenewalManager manager)
        {
            _repo = repo;
            _manager = manager;
        }

        // -----------------------------------------------------------
        // 1. Get all renewal records
        // -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        // -----------------------------------------------------------
        // 2. Get renewal by ID
        // -----------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return NotFound("Renewal not found");
            return Ok(result);
        }

        // -----------------------------------------------------------
        // 3. Create renewal record
        // -----------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(RenewalEntity renewal)
        {
            renewal.CreatedAt = DateTime.UtcNow;
            await _repo.AddAsync(renewal);

            return Ok(renewal);
        }

        // -----------------------------------------------------------
        // 4. Update renewal record
        // -----------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RenewalEntity updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound("Record not found");

            existing.ExpiryDate = updated.ExpiryDate;
            existing.ReminderDaysBefore = updated.ReminderDaysBefore;
            existing.Notes = updated.Notes;
            existing.Status = updated.Status;

            await _repo.UpdateAsync(existing);
            return Ok(existing);
        }

        // -----------------------------------------------------------
        // 5. Delete renewal record
        // -----------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok("Record deleted.");
        }

        // -----------------------------------------------------------
        // 6. Get renewals expiring in N days
        // -----------------------------------------------------------
        [HttpGet("expiring/{days}")]
        public async Task<IActionResult> GetExpiring(int days)
        {
            return Ok(await _manager.GetExpiringAsync(days));
        }

        // -----------------------------------------------------------
        // 7. Send reminders for renewals expiring in N days
        // -----------------------------------------------------------
        [HttpPost("send-reminders/{days}")]
        public async Task<IActionResult> SendReminders(int days)
        {
            await _manager.SendRemindersAsync(days);
            return Ok("Reminders sent.");
        }

        // -----------------------------------------------------------
        // 8. Mark renewal as completed
        // -----------------------------------------------------------
        [HttpPut("{id}/renewed")]
        public async Task<IActionResult> MarkRenewed(int id, [FromBody] string note)
        {
            await _manager.MarkRenewedAsync(id, note);
            return Ok("Marked as renewed.");
        }
    }
}
