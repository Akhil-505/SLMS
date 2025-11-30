using ReportingService.Models;
using System.Net.Http.Json;

namespace ReportingService.Repositories.Compliance
{
    public class ComplianceReportRepository : IComplianceReportRepository
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public ComplianceReportRepository(IHttpClientFactory factory, IConfiguration config)
        {
            _http = factory.CreateClient();
            _config = config;
        }

        public async Task<List<ComplianceEventModel>> GetReportAsync()
        {
            string url = $"{_config["ComplianceService:BaseUrl"]}/api/compliance/events";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var events = await response.Content.ReadFromJsonAsync<List<ComplianceEventModel>>();

            return events ?? new List<ComplianceEventModel>();
        }
    }
}
