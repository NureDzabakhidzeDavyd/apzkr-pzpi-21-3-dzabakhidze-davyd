using System.Linq.Expressions;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;
using Kolosok.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Kolosok.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly KolosokDbContext _context;

    protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

    public BaseRepository(KolosokDbContext context)
    {
        _context = context;
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetAllByFiltersAsync(
        SearchFilter searchFilter,
        params ISpecification<TEntity>[] specifications)
    {
        var query = DbSet.AsQueryable();

        if (specifications.Any())
        {
            query = ApplySpecification(query, specifications);
        }
        var result = await query
            .AsNoTracking()
            .Skip((searchFilter.PageNumber - 1) * searchFilter.PageSize)
            .Take(searchFilter.PageSize)
            .ToListAsync();
        return result;
    }

    public async Task<TEntity> GetByFiltersAsync(
        ISpecification<TEntity>[] specifications,
        params Expression<Func<TEntity, bool>>[] expressions)
    {
        var query = expressions.Aggregate(
            DbSet.AsQueryable(),
            (current, expression) => current.Where(expression)
        );

        if (specifications is not null && specifications.Any())
        {
            query = ApplySpecification(query, specifications);
        }

        return await query.FirstOrDefaultAsync();
    }


    private IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> query,
        params ISpecification<TEntity>[] specifications)
    {
        query = specifications.Aggregate(query, SpecificationEvaluator.GetQuery);
        return query;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var result = await DbSet.AddAsync(entity);
        return result.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
        {
            return false;
        }

        DbSet.Remove(entity);
        return true;
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await DbSet.AnyAsync(filter);
    }

    public TEntity Update(TEntity entity)
    {
        DbSet.Update(entity);
        return entity;
    }
    
    public async Task<bool> UpdatePropertiesAsync(
        Expression<Func<TEntity, bool>> filter,
        params (Expression<Func<TEntity, object>> property, object value)[] properties)
    {
        var entity = await DbSet.FirstOrDefaultAsync(filter);

        if (entity == null)
        {
            return false;
        }

        foreach (var (property, value) in properties)
        {
            _context.Entry(entity).Property(property).CurrentValue = value;
        }

        await _context.SaveChangesAsync();

        return true;
    }
}