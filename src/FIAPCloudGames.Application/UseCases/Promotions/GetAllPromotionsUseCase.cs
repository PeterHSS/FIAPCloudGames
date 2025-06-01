using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

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
        Log.Information("Retrieving all promotions...");

        IEnumerable<Promotion> promotions = await _promotionRepository.GetAllAsync(cancellationToken);

        Log.Information("Retrieved {Count} promotions.", promotions.Count());

        return promotions.Select(PromotionResponse.Create);
    }
}
