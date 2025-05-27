using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.DTOs.Users;

public record UserGameResponse(Guid Id, string Name, string Description, DateTime ReleasedAt, string Genre)
{
    public static UserGameResponse Create(Game game)
    {
        return new UserGameResponse(
            game.Id,
            game.Name,
            game.Description,
            game.ReleasedAt,
            game.Genre);
    }
}