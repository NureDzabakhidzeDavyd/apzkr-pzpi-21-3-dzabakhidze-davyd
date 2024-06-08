using System.Linq.Expressions;
using Kolosok.Domain.Entities;

namespace Kolosok.Application.Interfaces.Infrastructure;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public Task<IEnumerable<TEntity>> GetAllByFiltersAsync(
        SearchFilter searchFilter,
        params ISpecification<TEntity>[] specifications);

    public Task<TEntity> GetByFiltersAsync(ISpecification<TEntity>[]? specifications, 
        params Expression<Func<TEntity, bool>>[] expressions);
        
    public Task<TEntity> CreateAsync(TEntity entity);
    public TEntity Update(TEntity entity);
    public Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);

    public Task<bool> UpdatePropertiesAsync(
        Expression<Func<TEntity, bool>> filter,
        params (Expression<Func<TEntity, object>> property, object value)[] properties);
}