using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.DTOs.Games;

public record GameResponse(Guid Id, string Name, string Description, DateTime ReleasedAt, decimal Price, string Genre)
{
    public static GameResponse Create(Game game)
    {
        return new GameResponse(game.Id, game.Name, game.Description, game.ReleasedAt, game.Price, game.Genre);
    }
}

