using System.Linq.Expressions;
using FIAPCloudGames.Domain.Entities.Base;

namespace FIAPCloudGames.Domain.Abstractions.Repositories;

public interface IGenericRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetFirstOrDefaultAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetFirstOrDefaultAsyncWithFilter(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    void Update(TEntity entity, CancellationToken cancellationToken = default);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllWithFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
}
