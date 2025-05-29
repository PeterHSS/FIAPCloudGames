using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Infrastructure.Persistence.Repositories;

internal sealed class PromotionRepository : IPromotionRepository
{
    private readonly IGenericRepository<Promotion> _genericRepository;

    public PromotionRepository(IGenericRepository<Promotion> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task AddAsync(Promotion promotion, CancellationToken cancellationToken = default)
    {
        await _genericRepository.AddAsync(promotion, cancellationToken);
    }

    public async Task DeleteAsync(Promotion promotion, CancellationToken cancellationToken = default)
    {
        await _genericRepository.DeleteAsync(promotion, cancellationToken);
    }

    public async Task<IEnumerable<Promotion>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetAllAsync(cancellationToken: cancellationToken);
    }

    public async Task<Promotion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _genericRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task UpdateAsync(Promotion promotion, CancellationToken cancellationToken = default)
    {
        await _genericRepository.UpdateAsync(promotion, cancellationToken);
    }
}
