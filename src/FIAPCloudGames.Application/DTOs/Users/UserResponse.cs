namespace FIAPCloudGames.Application.DTOs.Users;

public record UserResponse(Guid Id, string Name, string Email, string Nickname, string Document, DateTime BirthDate, DateTime CreatedAt, DateTime? UpdatedAt);


