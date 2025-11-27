using ComplianceService.Models;
using System.Net.Http.Json;

namespace ComplianceService.Repositories.Inventory
{
    public class InstallationDataRepository : IInstallationDataRepository
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public InstallationDataRepository(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<List<InstalledSoftwareDto>> GetAllInstallationsAsync()
        {
            string url = $"{_config["InventoryService:BaseUrl"]}/api/installations";
            var res = await _http.GetAsync(url);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<List<InstalledSoftwareDto>>() ?? new List<InstalledSoftwareDto>();
        }
    }
}
