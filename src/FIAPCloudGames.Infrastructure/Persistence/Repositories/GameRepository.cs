using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Infrastructure.Persistence.Repositories;

internal sealed class GameRepository : IGameRepository
{
    private readonly IGenericRepository<Game> _genericRepository;

    public GameRepository(IGenericRepository<Game> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task AddAsync(Game game, CancellationToken cancellationToken = default)
    {
        await _genericRepository.AddAsync(game, cancellationToken);
    }

    public async Task DeleteAsync(Game game, CancellationToken cancellationToken = default)
    {
        await _genericRepository.DeleteAsync(game, cancellationToken);
    }

    public async Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetAllAsync(cancellationToken: cancellationToken);
    }

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Game game, CancellationToken cancellationToken = default)
    {
        await _genericRepository.UpdateAsync(game, cancellationToken);
    }
}
