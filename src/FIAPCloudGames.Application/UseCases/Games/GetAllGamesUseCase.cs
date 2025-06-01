using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Application.UseCases.Games;

public sealed class GetAllGamesUseCase
{
    private readonly IGameRepository _gameRepository;

    public GetAllGamesUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IEnumerable<GameResponse>> HandleAsync(CancellationToken cancellationToken)
    {
        Log.Information("Retrieving all games...");

        IEnumerable<Game> games = await _gameRepository.GetAllAsync(cancellationToken);

        Log.Information("Retrieved {Count} games.", games.Count());

        return games.Select(GameResponse.Create);
    }
}
