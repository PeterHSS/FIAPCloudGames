using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Domain.Abstractions.Repositories;

public interface IPromotionRepository
{
    Task<IEnumerable<Promotion>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Promotion promotion, CancellationToken cancellationToken = default);
    void Update(Promotion promotion, CancellationToken cancellationToken = default);
    void Delete(Promotion promotion, CancellationToken cancellationToken = default);
    Task<Promotion?> GetByIdWithGamesAsync(Guid id, CancellationToken cancellationToken = default);
}
