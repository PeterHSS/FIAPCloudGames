using System.Data;
using FIAPCloudGames.Application.DTOs.Promotion;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

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

        try
        {
            Promotion promotion = Promotion.Create(request.Name, request.StartDate, request.EndDate, request.DiscountPercentage, request.Description);

            await _promotionRepository.AddAsync(promotion, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (request.GamesId?.Any() ?? false)
            {
                IEnumerable<Game> retrievedGames = await _gameRepository.GetByIdListAsync(request.GamesId, cancellationToken);

                List<Guid> missingGameIds = request.GamesId.Except(retrievedGames.Select(game => game.Id)).ToList();

                if (missingGameIds.Any())
                    throw new ArgumentException($"Games with IDs {string.Join(", ", missingGameIds)} do not exist.");

                foreach (Game game in retrievedGames)
                    game.ApplyPromotion(promotion);

                _gameRepository.UpdateRange(retrievedGames);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();

            throw;
        }
    }
}
