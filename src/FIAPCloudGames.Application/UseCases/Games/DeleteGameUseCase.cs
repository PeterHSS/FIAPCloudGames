using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Games;

public sealed class DeleteGameUseCase
{
    private readonly IGameRepository _gameRepository;

    public DeleteGameUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Game? game = await _gameRepository.GetByIdAsync(id, cancellationToken);
        
        if (game is null)
            throw new KeyNotFoundException($"Game with ID {id} not found.");

        game.Delete();

        await _gameRepository.UpdateAsync(game, cancellationToken);
    }
}
