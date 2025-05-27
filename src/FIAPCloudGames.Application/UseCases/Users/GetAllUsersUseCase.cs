using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.UseCases.Users;

public sealed class GetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<User> users = await _userRepository.GetAllWithGamesAsync(cancellationToken);

        return
            users
            .Select(user => new UserResponse(
                user.Id,
                user.Name,
                user.Email,
                user.Nickname,
                user.Document,
                user.BirthDate,
                user.CreatedAt,
                user.UpdatedAt,
                user.Games.Select(UserGameResponse.Create)));
    }
}
