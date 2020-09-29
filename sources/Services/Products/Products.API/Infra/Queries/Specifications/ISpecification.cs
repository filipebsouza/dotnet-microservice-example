using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Products.API.Infra.Filters;

namespace Base.Infra.Queries.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        PaginationFilter Pagination { get; set; }
    }
}