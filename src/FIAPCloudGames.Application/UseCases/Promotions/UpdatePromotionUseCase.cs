using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class UpdatePromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePromotionUseCase(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
    {
        _promotionRepository = promotionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid id, UpdatePromotionRequest request, CancellationToken cancellationToken = default)
    {
        Promotion? promotion = await _promotionRepository.GetByIdAsync(id, cancellationToken);

        if (promotion is null)
            throw new KeyNotFoundException($"Promotion with ID {id} not found.");

        promotion.Update(request.Name, request.StartDate, request.EndDate, request.DiscountPercentage, request.Description);

        _promotionRepository.Update(promotion, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
