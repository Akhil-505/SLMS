using System.Net.Http.Json;
using AuthService.Models;

namespace AuthService.Services
{
    public class HttpEmailClient
    {
        private readonly HttpClient _http;

        public HttpEmailClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<HttpResponseMessage> SendEmailAsync(EmailRequest request)
        {
            return await _http.PostAsJsonAsync("/api/email/send", request);
        }
    }
}
