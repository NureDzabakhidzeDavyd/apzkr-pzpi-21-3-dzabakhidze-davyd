using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kolosok.Infrastructure.Specifications;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> query,
        ISpecification<TEntity> specification
    ) where TEntity : BaseEntity
    {
        query = specification.StringIncludes.Aggregate(query, (current, include) =>
            current.Include(include)
        );

        query = specification.Includes.Aggregate(query, (current, include) =>
            current.Include(include)
        );          

        return query;
    }
}