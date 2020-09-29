using Microsoft.Extensions.DependencyInjection;
using Products.Infra.Queries;
using Products.Infra.Queries.Interfaces;

namespace Products.API.Infra.IoC
{
    public class QueriesServiceResolver
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGetAllProductsQuery), typeof(GetAllProductsQuery));
        }
    }
}