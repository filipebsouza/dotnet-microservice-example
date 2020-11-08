using Common.Infra.Queries.Specifications;
using Products.API.Domain.Entities;

namespace Products.API.Infra.Queries.Filters
{
    public class ProductContainsNameSpecification : BaseSpecification<Product>
    {
        public ProductContainsNameSpecification(string productName)
        {
            if (AreParametersValid(productName))
                Criteria = where => where.Name.Contains(productName);
        }

        private bool AreParametersValid(string productName) => !string.IsNullOrWhiteSpace(productName);
    }
}