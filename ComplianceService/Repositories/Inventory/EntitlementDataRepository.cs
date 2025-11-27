using ComplianceService.Models;
using System.Net.Http.Json;

namespace ComplianceService.Repositories.Inventory
{
    public class EntitlementDataRepository : IEntitlementDataRepository
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public EntitlementDataRepository(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<List<EntitlementDto>> GetAllEntitlementsAsync()
        {
            string url = $"{_config["InventoryService:BaseUrl"]}/api/entitlements";
            var res = await _http.GetAsync(url);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<List<EntitlementDto>>() ?? new List<EntitlementDto>();
        }
    }
}
