using Common.Infra.Queries.Specifications;
using Products.API.Domain.Entities;

namespace Products.Infra.Queries.Filters
{
    public class ProductClassificationSpecification : BaseSpecification<Product>
    {
        public ProductClassificationSpecification(int classification)
        {
            Criteria = where => 
                where.Classification.HasValue &&
                where.Classification >= classification;
        }
    }
}