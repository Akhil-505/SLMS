using ReportingService.Models;
using System.Net.Http.Json;

namespace ReportingService.Repositories.Inventory
{
    public class EntitlementRepository : IEntitlementRepository
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public EntitlementRepository(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<List<EntitlementDto>> GetAllAsync()
        {
            string url = $"{_config["InventoryService:BaseUrl"]}/api/entitlements";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<EntitlementDto>>()
                ?? new List<EntitlementDto>();
        }
    }
}
