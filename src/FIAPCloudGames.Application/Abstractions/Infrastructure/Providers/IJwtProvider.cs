using FIAPCloudGames.Domain.Entities;

namespace FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;

public interface IJwtProvider
{
    string Create(User user);
}