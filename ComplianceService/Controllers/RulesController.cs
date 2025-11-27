using ComplianceService.Models.Database;
using ComplianceService.Repositories.Database;
using Microsoft.AspNetCore.Mvc;

namespace ComplianceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RulesController : ControllerBase
    {
        private readonly IComplianceRuleRepository _repo;

        public RulesController(IComplianceRuleRepository repo)
        {
            _repo = repo;
        }

        // -----------------------------------------------------------
        // 1. Get all rules
        // -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        // -----------------------------------------------------------
        // 2. Create rule
        // -----------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(ComplianceRuleEntity rule)
        {
            rule.CreatedAt = DateTime.UtcNow;
            await _repo.AddAsync(rule);
            return Ok(rule);
        }

        // -----------------------------------------------------------
        // 3. Update rule
        // -----------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ComplianceRuleEntity updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound("Rule not found");

            existing.Value = updated.Value;
            existing.Enabled = updated.Enabled;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.Severity = updated.Severity;

            await _repo.UpdateAsync(existing);
            return Ok(existing);
        }

        // -----------------------------------------------------------
        // 4. Delete rule
        // -----------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok("Rule deleted.");
        }
    }
}
