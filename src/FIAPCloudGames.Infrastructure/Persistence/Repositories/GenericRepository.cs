using System.Linq.Expressions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities.Base;
using FIAPCloudGames.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infrastructure.Persistence.Repositories;

internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
{
    private readonly FIAPCloudGamesDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(FIAPCloudGamesDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();    
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> entities = _dbSet.AsNoTracking();

        if (filter is not null)
            entities = entities.Where(filter);

        return await entities.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> GetFirstOrDefaultAsyncWithFilter(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync(cancellationToken);
    }
}
