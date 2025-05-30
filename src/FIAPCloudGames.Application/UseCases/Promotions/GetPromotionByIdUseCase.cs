using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class GetPromotionByIdUseCase
{
    private readonly IPromotionRepository _promotionRepository;

    public GetPromotionByIdUseCase(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<PromotionResponse> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Promotion? promotion = await _promotionRepository.GetByIdWithGamesAsync(id, cancellationToken);

        if (promotion is null)
            throw new KeyNotFoundException($"Promotion with ID {id} not found.");

        PromotionResponse promotionResponse = PromotionResponse.Create(promotion);

        return promotionResponse;
    }
}
