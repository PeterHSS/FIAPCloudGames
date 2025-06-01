using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

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
        Log.Information("Retrieving promotion with ID {Id}.", id);

        Promotion? promotion = await _promotionRepository.GetByIdWithGamesAsync(id, cancellationToken);

        if (promotion is null)
        {
            Log.Warning("Promotion with ID {Id} not found.", id);

            throw new KeyNotFoundException($"Promotion with ID {id} not found.");
        }

        PromotionResponse promotionResponse = PromotionResponse.Create(promotion);

        Log.Information("Promotion with ID {PromotionId} retrieved successfully.", id);

        return promotionResponse;
    }
}
