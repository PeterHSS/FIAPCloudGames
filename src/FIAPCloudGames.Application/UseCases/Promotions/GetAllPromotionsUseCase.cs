using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class GetAllPromotionsUseCase
{
    private readonly IPromotionRepository _promotionRepository;

    public GetAllPromotionsUseCase(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<IEnumerable<PromotionResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Promotion> promotions = await _promotionRepository.GetAllAsync(cancellationToken);

        return promotions.Select(PromotionResponse.Create);
    }
}
