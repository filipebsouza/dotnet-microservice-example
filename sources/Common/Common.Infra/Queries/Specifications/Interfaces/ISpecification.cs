using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Infra.Queries.Filters;

namespace Common.Infra.Queries.Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        PaginationFilter Pagination { get; set; }
    }
}