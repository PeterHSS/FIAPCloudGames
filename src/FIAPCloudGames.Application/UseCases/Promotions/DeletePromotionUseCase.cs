using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

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
        Log.Information("Deleting promotion with ID {Id}", id);

        Promotion? promotion = await _promotionRepository.GetByIdAsync(id, cancellationToken);

        if (promotion is null)
        {
            Log.Warning("Promotion with ID {Id} not found", id);

            throw new KeyNotFoundException($"Promotion with ID {id} not found.");
        }

        _promotionRepository.Delete(promotion, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Promotion with ID {Id} deleted successfully", id);
    }
}
