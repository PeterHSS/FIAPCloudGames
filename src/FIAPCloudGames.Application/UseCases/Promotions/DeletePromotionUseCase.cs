using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class DeletePromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePromotionUseCase(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
    {
        _promotionRepository = promotionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Promotion? promotion = await _promotionRepository.GetByIdAsync(id, cancellationToken);

        if (promotion is null)
            throw new KeyNotFoundException($"Promotion with ID {id} not found.");

        _promotionRepository.Delete(promotion, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
