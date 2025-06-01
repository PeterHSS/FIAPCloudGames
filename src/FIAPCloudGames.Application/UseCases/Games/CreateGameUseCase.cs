using FIAPCloudGames.Application.DTOs.Games;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Serilog;

namespace FIAPCloudGames.Application.UseCases.Games;

public sealed class CreateGameUseCase
{
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGameUseCase(IGameRepository gameRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(CreateGameRequest request, CancellationToken cancellationToken)
    {
        Log.Information("Creating game with name: {GameName}", request.Name);

        Game game = Game.Create(request.Name, request.Description, request.ReleasedAt, request.Price, request.Genre);

        await _gameRepository.AddAsync(game, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log.Information("Game created successfully with ID: {GameId}", game.Id);
    }
}
