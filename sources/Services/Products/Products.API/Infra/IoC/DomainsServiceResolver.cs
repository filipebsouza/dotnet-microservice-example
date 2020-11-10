using Microsoft.Extensions.DependencyInjection;
using Products.API.Domain.Service;
using Products.API.Domain.Service.Interfaces;

namespace Products.API.Infra.IoC
{
    public class DomainsServiceResolver
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ISaveProductService), typeof(SaveProductService));
        }
    }
}