using FIAPCloudGames.Application.DTOs.Promotion;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

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
        Promotion promotion = Promotion.Create(request.Name, request.StartDate, request.EndDate, request.DiscountPercentage, request.Description);

        await _promotionRepository.AddAsync(promotion);
    }
}
