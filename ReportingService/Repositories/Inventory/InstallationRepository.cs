using ReportingService.Models;
using System.Net.Http.Json;

namespace ReportingService.Repositories.Inventory
{
    public class InstallationRepository : IInstallationRepository
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public InstallationRepository(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<List<InstalledSoftwareDto>> GetAllAsync()
        {
            string url = $"{_config["InventoryService:BaseUrl"]}/api/installations";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<InstalledSoftwareDto>>()
                ?? new List<InstalledSoftwareDto>();
        }
    }
}
