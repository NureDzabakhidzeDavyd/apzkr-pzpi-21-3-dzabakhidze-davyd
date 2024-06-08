using System.Linq.Expressions;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;

namespace Kolosok.Infrastructure.Specifications;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity
{
    public List<Expression<Func<TEntity, bool>>> Criteria { get; set; } = new();
    public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
    public List<string> StringIncludes { get; set; } = new();

    protected void AddManyIncludes(params Expression<Func<TEntity, object>>[] expressions)
    {
        Includes.AddRange(expressions);
    }
        
    protected void AddIncludes(string includeString)
    {
        StringIncludes.Add(includeString);
    }

    protected void AddIncludes(params Expression<Func<TEntity, object>>[] expressions)
    {
        Includes.AddRange(expressions);
    }
}