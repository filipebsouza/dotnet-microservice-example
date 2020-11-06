using System.Linq;
using System.Linq.Expressions;
using Common.Infra.Queries.Filters;
using Common.Infra.Queries.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.Infra.Queries.Specifications
{
    public static class QuerySpecificationExtensions
    {
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            if (spec.Pagination == null)
            {
                if (spec.Criteria != null)
                    return secondaryResult.Where(spec.Criteria);
                else
                    return secondaryResult;
            }
            else
            {
                var totalItens = secondaryResult
                    .Count();

                spec.Pagination.Total = totalItens;
                spec.Pagination.Pages = (totalItens + 1) / spec.Pagination.ItensPerPage;

                if (spec.Criteria != null)
                    secondaryResult = secondaryResult.Where(spec.Criteria);

                secondaryResult = secondaryResult
                    .Skip(spec.Pagination.ItensPerPage * (spec.Pagination.CurrentPage - 1))
                    .Take(spec.Pagination.ItensPerPage);

                var parameter = Expression.Parameter(typeof(T), "x");
                Expression property = Expression.Property(parameter, spec.Pagination.OrderByFieldName);
                var lambda = Expression.Lambda(property, parameter);
                var orderByMethod = typeof(Queryable).GetMethods().First(x =>
                    x.Name == (spec.Pagination.OrderBy == OrderByEnum.ASC ? "OrderBy" : "OrderByDescending") &&
                    x.GetParameters().Length == 2
                );
                var orderByGeneric = orderByMethod.MakeGenericMethod(typeof(T), property.Type);
                var result = orderByGeneric.Invoke(null, new object[] { secondaryResult, lambda });

                return (IOrderedQueryable<T>)result;
            }
        }
    }
}