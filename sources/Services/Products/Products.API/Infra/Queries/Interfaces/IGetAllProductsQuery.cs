using System.Linq;
using Products.API.Domain.Entities;
using Products.API.Infra.Filters;

namespace Products.API.Infra.Queries.Interfaces
{
    public interface IGetAllProductsQuery
    {
        IQueryable<Product> Get(ProductFilter filter);
    }
}