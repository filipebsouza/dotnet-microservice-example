using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Products.API.Infra.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        public static string HealthCheckName = "DatabaseHealthCheck";
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await Task.Run<HealthCheckResult>(() =>
            {
                return HealthCheckResult.Healthy();
            });
        }        
    }
}