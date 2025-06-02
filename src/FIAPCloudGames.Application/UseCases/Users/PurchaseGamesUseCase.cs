using FIAPCloudGames.Application.Abstractions.Infrastructure.Services;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class PurchaseGamesUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public PurchaseGamesUseCase(IUserRepository userRepository, IGameRepository gameRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task HandleAsync(UserPurchaseRequest request, CancellationToken cancellationToken = default)
    {
        Log.Information("Processing purchase for user with ID {UserId} for games with IDs {@GameId}.", _currentUserService.UserId, request);

        if (!request.gamesIds.Any())
        {
            Log.Warning("No game IDs provided for purchase by user with ID {UserId}.", _currentUserService.UserId);

            throw new ArgumentException("At least one game ID must be provided for purchase.");
        }


        User? user = await _userRepository.GetByIdWithGamesync(_currentUserService.UserId, cancellationToken);

        if (user is null)
        {
            Log.Warning("User with ID {UserId} not found.", _currentUserService.UserId);

            throw new KeyNotFoundException($"User with ID {_currentUserService} not found.");
        }

        IEnumerable<Game> gamesToPurchase = await _gameRepository.GetByIdListAsync(request.gamesIds, cancellationToken);

        if (!gamesToPurchase.Any())
        {
            Log.Warning("No games found for purchase with IDs {@GameIds}.", request.gamesIds);

            throw new KeyNotFoundException($"No games found for the provided IDs: {string.Join(", ", request.gamesIds)}.");
        }

        IEnumerable<Guid> missingGameIds = request.gamesIds.Except(gamesToPurchase.Select(game => game.Id));

        if (missingGameIds.Any())
        {
            Log.Warning("Some game IDs were not found: {@MissingGameIds}.", missingGameIds);

            throw new KeyNotFoundException($"The following game IDs were not found: {string.Join(", ", missingGameIds)}.");
        }

        foreach (Game game in gamesToPurchase)
        {
            if (user.HasPurchasedGame(game))
            {
                Log.Warning("User with ID {UserId} has already purchased game with ID {GameId}.", user.Id, game.Id);

                continue;
            }

            user.PurchaseGame(game);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
