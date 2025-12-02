using MailService.Models;
using MailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _service;

        public EmailController(IEmailService service)
        {
            _service = service;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] EmailRequest request)
        {
            bool ok = await _service.SendEmailAsync(request);
            return ok ? Ok("Email sent.") : StatusCode(500, "Sending failed.");
        }
    }
}
