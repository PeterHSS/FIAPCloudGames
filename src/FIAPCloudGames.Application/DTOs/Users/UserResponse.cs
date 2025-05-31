using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.DTOs.Users;

public record UserResponse(
    Guid Id,
    string Name,
    string Email,
    string Nickname,
    string Document,
    DateTime BirthDate,
    IEnumerable<UserGameResponse> Games)
{
    public static UserResponse Create(User user)
    {
        return new UserResponse(
            user.Id,
            user.Name,
            user.Email,
            user.Nickname,
            user.Document,
            user.BirthDate,
            user.Games.Select(UserGameResponse.Create));
    }
}