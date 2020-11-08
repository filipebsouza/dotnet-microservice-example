using Common.Infra.Queries.Filters;
using Common.Infra.Queries.Specifications;

namespace Products.API.Infra.Queries.Filters
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