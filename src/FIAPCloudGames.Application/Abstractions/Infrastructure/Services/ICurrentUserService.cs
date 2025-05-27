namespace FIAPCloudGames.Application.Abstractions.Infrastructure.Services;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}

