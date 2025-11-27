using ComplianceService.Models;
using System.Net.Http.Json;

namespace ComplianceService.Repositories.Inventory
{
    public class LicenseDataRepository : ILicenseDataRepository
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public LicenseDataRepository(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<List<LicenseDto>> GetAllLicensesAsync()
        {
            string url = $"{_config["InventoryService:BaseUrl"]}/api/licenses";
            var res = await _http.GetAsync(url);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<List<LicenseDto>>() ?? new List<LicenseDto>();
        }
    }
}
