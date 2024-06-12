using System.Linq.Expressions;
using Kolosok.Domain.Entities;

namespace Kolosok.Application.Interfaces.Infrastructure;

public interface ISpecification<TEntity> where TEntity : BaseEntity
{
    public List<Expression<Func<TEntity, bool>>> Criteria { get; set; }
    public List<Expression<Func<TEntity, object>>> Includes { get; set; }
    public List<string> StringIncludes { get; set; }
}