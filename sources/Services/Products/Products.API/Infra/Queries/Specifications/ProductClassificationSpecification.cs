using Base.Infra.Queries.Specifications;
using Products.API.Domain;

namespace Products.API.Infra.Queries.Filters
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