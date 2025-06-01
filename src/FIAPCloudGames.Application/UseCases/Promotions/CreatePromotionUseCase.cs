using System.Data;
using FIAPCloudGames.Application.DTOs.Promotion;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class CreatePromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePromotionUseCase(IPromotionRepository promotionRepository, IGameRepository gameRepository, IUnitOfWork unitOfWork)
    {
        _promotionRepository = promotionRepository;
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(CreatePromotionRequest request, CancellationToken cancellationToken = default)
    {
        using IDbTransaction transaction = _unitOfWork.BeginTransaction(cancellationToken);

        Log.Information("Starting transaction for creating promotion with name: {PromotionName}", request.Name);

        try
        {
            Promotion promotion = Promotion.Create(request.Name, request.StartDate, request.EndDate, request.DiscountPercentage, request.Description);

            await _promotionRepository.AddAsync(promotion, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            Log.Information("Promotion created successfully with ID: {PromotionId}", promotion.Id);

            if (request.GamesId?.Any() ?? false)
            {
                Log.Information("Retrieving games for promotion with IDs: {GameIds}", string.Join(", ", request.GamesId));

                IEnumerable<Game> retrievedGames = await _gameRepository.GetByIdListAsync(request.GamesId, cancellationToken);

                List<Guid> missingGameIds = request.GamesId.Except(retrievedGames.Select(game => game.Id)).ToList();

                Log.Information("Found {Count} games for promotion, missing {MissingCount} games", retrievedGames.Count(), missingGameIds.Count);

                if (missingGameIds.Any())
                {
                    Log.Warning("Games with IDs {MissingGameIds} do not exist", string.Join(", ", missingGameIds));

                    throw new ArgumentException($"Games with IDs {string.Join(", ", missingGameIds)} do not exist.");
                }

                foreach (Game game in retrievedGames)
                    game.ApplyPromotion(promotion);

                _gameRepository.UpdateRange(retrievedGames);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                Log.Information("Games updated with promotion successfully");
            }

            transaction.Commit();

            Log.Information("Transaction committed successfully for creating promotion with name: {PromotionName}", request.Name);
        }
        catch (Exception)
        {
            transaction.Rollback();

            Log.Warning("Transaction rolled back due to an error while creating promotion with name: {PromotionName}", request.Name);

            throw;
        }
    }
}
