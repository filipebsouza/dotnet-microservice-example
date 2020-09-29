using System.Linq;
using Products.API.Domain;
using Products.API.Infra.Filters;

namespace Products.Infra.Queries.Interfaces
{
    public interface IGetAllProductsQuery
    {
        IQueryable<Product> Get(ProductFilter filter);
    }
}