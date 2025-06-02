using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infrastructure.Persistence.Repositories;

internal sealed class GameRepository : IGameRepository
{
    private readonly IGenericRepository<Game> _genericRepository;
    private readonly FIAPCloudGamesDbContext _dbContext;

    public GameRepository(IGenericRepository<Game> genericRepository, FIAPCloudGamesDbContext dbContext)
    {
        _genericRepository = genericRepository;
        _dbContext = dbContext;
    }

    public async Task AddAsync(Game game, CancellationToken cancellationToken = default)
    {
        await _genericRepository.AddAsync(game, cancellationToken);
    }

    public void Delete(Game game, CancellationToken cancellationToken = default)
    {
        _genericRepository.Delete(game, cancellationToken);
    }

    public async Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetAllAsync(cancellationToken: cancellationToken);
    }

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Game>> GetByIdListAsync(IEnumerable<Guid> guids, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Games
            .Where(game => guids.Contains(game.Id))
            .ToListAsync(cancellationToken);
    }

    public void Update(Game game, CancellationToken cancellationToken = default)
    {
        _genericRepository.Update(game, cancellationToken);
    }

    public void UpdateRange(IEnumerable<Game> retrievedGames)
    {
        _genericRepository.UpdateRange(retrievedGames);
    }
}
