using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Games;

public sealed class UpdateGameUseCase
{
    private readonly IGameRepository _gameRepository;

    public UpdateGameUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task HandleAsync(Guid id, UpdateGameRequest request, CancellationToken cancellationToken = default)
    {
        Game? game = await _gameRepository.GetByIdAsync(id, cancellationToken);

        if (game is null)
            throw new KeyNotFoundException($"Game with ID {id} not found.");

        game.Update(request.Name, request.Description, request.ReleasedAt, request.Price, request.Genre);

        await _gameRepository.UpdateAsync(game);
    }
}
