using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

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
        Log.Information("Start updating promotion. {@PromotionId} {@Request}", id, request);

        Promotion? promotion = await _promotionRepository.GetByIdAsync(id, cancellationToken);

        if (promotion is null)
        {
            Log.Warning("Promotion not found. {@PromotionId}", id);

            throw new KeyNotFoundException($"Promotion with ID {id} not found.");
        }

        promotion.Update(request.Name, request.StartDate, request.EndDate, request.DiscountPercentage, request.Description);

        _promotionRepository.Update(promotion, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Promotion updated successfully. {@PromotionId}", id);
    }
}
