using ReportingService.Models;
using System.Net.Http.Json;

namespace ReportingService.Repositories.Inventory
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public LicenseRepository(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<List<LicenseDto>> GetAllAsync()
        {
            string url = $"{_config["InventoryService:BaseUrl"]}/api/licenses";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<LicenseDto>>()
                ?? new List<LicenseDto>();
        }
    }
}
