using System.Linq;
using Base.Infra.Queries.Specifications;
using Products.API.Domain;
using Products.API.Infra.Contexts;
using Products.API.Infra.Filters;
using Products.Infra.Queries.Filters;
using Products.Infra.Queries.Interfaces;

namespace Products.Infra.Queries
{
    public class GetAllProductsQuery : IGetAllProductsQuery
    {
        private readonly ProductContext _productContext;

        public GetAllProductsQuery(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public IQueryable<Product> Get(ProductFilter filter)
        {
            return _productContext.Set<Product>()
                .Specify(new ProductContainsNameSpecification(filter.Name))
                .Specify(new ProductRangeByPriceSpecification(filter.MinPrice, filter.MaxPrice))
                .Specify(new PaginationSpecification<Product>());
        }
    }
}