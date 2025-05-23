namespace FIAPCloudGames.Application.DTOs.Users;

public record CreateUserRequest(string Name, string Email, string Password, string Nickname, string Document, DateTime BirthDate);
