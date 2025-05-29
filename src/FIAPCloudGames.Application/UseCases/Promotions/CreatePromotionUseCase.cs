using FIAPCloudGames.Application.DTOs.Promotion;
using FIAPCloudGames.Domain.Abstractions.Repositories;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class CreatePromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;

    public CreatePromotionUseCase(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task HandleAsync(CreatePromotionRequest request)
    {
        throw new NotImplementedException();
    }
}
