namespace FIAPCloudGames.Application.DTOs.Games;

public record CreateGameRequest(string Name, string Description, DateTime ReleasedAt, decimal Price, string Genre);