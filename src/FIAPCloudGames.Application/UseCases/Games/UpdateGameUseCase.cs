using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Games;

public sealed class UpdateGameUseCase
{
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGameUseCase(IGameRepository gameRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(Guid id, UpdateGameRequest request, CancellationToken cancellationToken = default)
    {
        Game? game = await _gameRepository.GetByIdAsync(id, cancellationToken);

        if (game is null)
            throw new KeyNotFoundException($"Game with ID {id} not found.");

        game.Update(request.Name, request.Description, request.ReleasedAt, request.Price, request.Genre);

        _gameRepository.Update(game);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
