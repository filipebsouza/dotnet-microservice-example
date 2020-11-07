using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Products.API.Infra.HealthChecks;

namespace Products.API.Infra.IoC
{
    public class HealthChecksServiceResolver
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck(
                    DatabaseHealthCheck.HealthCheckName,
                    new DatabaseHealthCheck(),
                    HealthStatus.Unhealthy,
                    new string[] { "orderingdb" }
                );
        }
    }
}