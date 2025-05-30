using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Games;

public sealed class GetGameByIdUseCase
{
    private readonly IGameRepository _gameRepository;

    public GetGameByIdUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GameResponse> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Game? game = await _gameRepository.GetByIdAsync(id, cancellationToken);

        if (game is null)
            throw new KeyNotFoundException($"Game with ID {id} not found.");

        GameResponse gameResponse = GameResponse.Create(game);

        return gameResponse;
    }
}

