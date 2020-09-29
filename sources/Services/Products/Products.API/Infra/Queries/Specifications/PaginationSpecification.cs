using Base.Infra.Queries.Specifications;
using Products.API.Infra.Filters;

namespace Products.Infra.Queries.Filters
{
    public class PaginationSpecification<T> : BaseSpecification<T>
        where T : class
    {
        public PaginationSpecification(PaginationFilter paginationFilter = null)
        {
            Pagination = paginationFilter ?? new PaginationFilter();
        }
    }
}