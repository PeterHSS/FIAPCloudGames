using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Application.UseCases.Promotions;

public sealed class AddGameToPromotionUseCase
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddGameToPromotionUseCase(IPromotionRepository promotionRepository, IGameRepository gameRepository, IUnitOfWork unitOfWork)
    {
        _promotionRepository = promotionRepository;
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid promotionId, Guid gameId, CancellationToken cancellationToken = default)
    {
        Log.Information("Adding game with ID {GameId} to promotion with ID {PromotionId}.", gameId, promotionId);

        Promotion? promotion = await _promotionRepository.GetByIdAsync(promotionId, cancellationToken);

        if (promotion is null)
        {
            Log.Warning("Promotion with ID {PromotionId} not found.", promotionId);

            throw new KeyNotFoundException($"Promotion with ID {promotionId} not found.");
        }

        Game? game = await _gameRepository.GetByIdAsync(gameId, cancellationToken);

        if (game is null)
        {
            Log.Warning("Game with ID {GameId} not found.", gameId);

            throw new KeyNotFoundException($"Game with ID {gameId} not found.");
        }

        game.ApplyPromotion(promotion);

        _gameRepository.Update(game, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Game with ID {GameId} added to promotion with ID {PromotionId}.", gameId, promotionId);
    }
}
