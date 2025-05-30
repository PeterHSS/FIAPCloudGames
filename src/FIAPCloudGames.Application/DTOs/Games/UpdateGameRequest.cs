namespace FIAPCloudGames.Application.DTOs.Games;

public record UpdateGameRequest(string Name, string Description, DateTime ReleasedAt, decimal Price, string Genre);

