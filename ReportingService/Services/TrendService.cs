using ReportingService.Models;

namespace ReportingService.Services
{
    public class TrendService : ITrendService
    {
        // In real world you would store monthly snapshots.
        // For now we simulate trend for frontend charts.

        public Task<List<TrendPoint>> GetInstallationTrendAsync()
        {
            var data = new List<TrendPoint>
            {
                new TrendPoint { Label = "Jan", Value = 120 },
                new TrendPoint { Label = "Feb", Value = 140 },
                new TrendPoint { Label = "Mar", Value = 160 },
                new TrendPoint { Label = "Apr", Value = 180 }
            };

            return Task.FromResult(data);
        }

        public Task<List<TrendPoint>> GetComplianceTrendAsync()
        {
            var data = new List<TrendPoint>
            {
                new TrendPoint { Label = "Jan", Value = 82 },
                new TrendPoint { Label = "Feb", Value = 87 },
                new TrendPoint { Label = "Mar", Value = 89 },
                new TrendPoint { Label = "Apr", Value = 91 }
            };

            return Task.FromResult(data);
        }
    }
}
