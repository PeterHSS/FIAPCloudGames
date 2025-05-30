using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

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
        IEnumerable<Game> games = await _gameRepository.GetAllAsync(cancellationToken);

        return games.Select(GameResponse.Create);
    }
}
