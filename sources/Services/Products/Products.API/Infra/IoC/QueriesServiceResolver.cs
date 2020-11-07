using Microsoft.Extensions.DependencyInjection;
using Products.API.Infra.Queries;
using Products.API.Infra.Queries.Interfaces;

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