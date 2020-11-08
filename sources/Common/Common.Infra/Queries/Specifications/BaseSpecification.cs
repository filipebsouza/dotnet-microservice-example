using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Infra.Queries.Filters;
using Common.Infra.Queries.Specifications.Interfaces;

namespace Common.Infra.Queries.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public PaginationFilter Pagination { get; set; }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}