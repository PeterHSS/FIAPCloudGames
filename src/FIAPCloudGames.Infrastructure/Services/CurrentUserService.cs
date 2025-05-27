using System.IdentityModel.Tokens.Jwt;
using FIAPCloudGames.Application.Abstractions.Infrastructure.Services;
using Microsoft.AspNetCore.Http;

namespace FIAPCloudGames.Infrastructure.Services;

internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId 
        => Guid.TryParse(_httpContextAccessor?.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)? .Value, out Guid id)
            ? id
            : throw new InvalidOperationException("User ID not found in the current context.");
}
