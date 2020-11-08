using Common.Infra.Queries.Specifications;
using Products.API.Domain.Entities;

namespace Products.API.Infra.Queries.Filters
{
    public class ProductRangeByPriceSpecification : BaseSpecification<Product>
    {
        public ProductRangeByPriceSpecification(decimal? minPrice, decimal? maxPrice)
        {
            Criteria = where =>
                where.Price.HasValue &&
                minPrice.HasValue ? where.Price.Value >= minPrice.Value : true &&
                maxPrice.HasValue ? where.Price.Value <= maxPrice.Value : true;
        }
    }
}