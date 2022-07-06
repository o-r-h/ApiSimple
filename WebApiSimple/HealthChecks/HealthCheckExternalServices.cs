
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiSimple.HealthChecks
{
    public class HealthCheckExternalServices : IHealthCheck
    {
        private readonly IHttpClientFactory httpClientFactory;

   
        public HealthCheckExternalServices(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync("https://restcountries.com/v2/name/peru");
                if (response.IsSuccessStatusCode)
                {
                    return HealthCheckResult.Healthy($"API is running.");
                }

                return HealthCheckResult.Unhealthy("API is not running");
            }
        }

    }
}
