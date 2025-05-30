using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class DeletePromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;

    public DeletePromotionUseCase(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Promotion? promotion = await _promotionRepository.GetByIdAsync(id, cancellationToken);

        if (promotion is null)
            throw new KeyNotFoundException($"Promotion with ID {id} not found.");

        await _promotionRepository.DeleteAsync(promotion, cancellationToken);
    }
}
