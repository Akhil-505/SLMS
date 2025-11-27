using InventoryService.Models;
using InventoryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorContractsController : ControllerBase
    {
        private readonly IVendorContractService _service;

        public VendorContractsController(IVendorContractService service)
        {
            _service = service;
        }

        [HttpGet("{licenseId}")]
        public async Task<IActionResult> Get(int licenseId)
        {
            var contract = await _service.GetByLicenseIdAsync(licenseId);
            return contract == null ? NotFound() : Ok(contract);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VendorContract contract)
        {
            var created = await _service.CreateAsync(contract);
            return Ok(created);
        }

        [HttpPut("{licenseId}")]
        public async Task<IActionResult> Update(int licenseId, VendorContract update)
        {
            try
            {
                return Ok(await _service.UpdateAsync(licenseId, update));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{licenseId}")]
        public async Task<IActionResult> Delete(int licenseId)
        {
            var ok = await _service.DeleteAsync(licenseId);
            return ok ? NoContent() : NotFound();
        }
    }
}
