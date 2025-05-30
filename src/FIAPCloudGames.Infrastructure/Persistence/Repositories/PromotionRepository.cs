using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAPCloudGames.Infrastructure.Persistence.Repositories;

internal sealed class PromotionRepository : IPromotionRepository
{
    private readonly IGenericRepository<Promotion> _genericRepository;
    private readonly FIAPCloudGamesDbContext _context;

    public PromotionRepository(IGenericRepository<Promotion> genericRepository, FIAPCloudGamesDbContext context)
    {
        _genericRepository = genericRepository;
        _context = context;
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

    public async Task<Promotion?> GetByIdWithGamesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Promotions
            .AsNoTracking()
            .Include(promotion => promotion.Games)
            .FirstOrDefaultAsync(promotion => promotion.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Promotion promotion, CancellationToken cancellationToken = default)
    {
        await _genericRepository.UpdateAsync(promotion, cancellationToken);
    }
}
