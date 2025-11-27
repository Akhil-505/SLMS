using ComplianceService.Models;
using System.Net.Http.Json;

namespace ComplianceService.Repositories.Inventory
{
    public class CatalogDataRepository : ICatalogDataRepository
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public CatalogDataRepository(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<List<SoftwareCatalogDto>> GetCatalogAsync()
        {
            string url = $"{_config["InventoryService:BaseUrl"]}/api/catalog";
            var res = await _http.GetAsync(url);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<List<SoftwareCatalogDto>>() ?? new List<SoftwareCatalogDto>();
        }
    }
}
