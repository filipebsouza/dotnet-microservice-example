using Base.Infra.Queries.Specifications;
using Products.API.Domain;

namespace Products.Infra.Queries.Filters
{
    public class ProductContainsNameSpecification : BaseSpecification<Product>
    {
        public ProductContainsNameSpecification(string productName)
        {
            Criteria = where => where.Name.Contains(productName);
        }
    }
}