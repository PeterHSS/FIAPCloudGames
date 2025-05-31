using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Domain.Abstractions.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Game game, CancellationToken cancellationToken = default);
    void Update(Game game, CancellationToken cancellationToken = default);
    void Delete(Game game, CancellationToken cancellationToken = default);
    Task<IEnumerable<Game>> GetByIdListAsync(IEnumerable<Guid> guids, CancellationToken cancellationToken = default);
    void UpdateRange(IEnumerable<Game> retrievedGames);
}
