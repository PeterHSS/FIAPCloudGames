using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class UpdatePromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;

    public UpdatePromotionUseCase(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task HandleAsync(Guid id, UpdatePromotionRequest request)
    {
        Promotion? promotion = await _promotionRepository.GetByIdAsync(id);

        if (promotion is null)
            throw new KeyNotFoundException($"Promotion with ID {id} not found.");

        promotion.Update(request.Name, request.StartDate, request.EndDate, request.DiscountPercentage, request.Description);

        await _promotionRepository.UpdateAsync(promotion);
    }
}
