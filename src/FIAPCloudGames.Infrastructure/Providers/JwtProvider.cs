using System.Security.Claims;
using System.Text;
using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Infrastructure.Settings;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace FIAPCloudGames.Infrastructure.Providers;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;

    public JwtProvider(JwtSettings jwtSetting)
    {
        _jwtSettings = jwtSetting;
    }

    public string Create(User user)
    {
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                ]),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            SigningCredentials = signingCredentials,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };


        JsonWebTokenHandler jsonWebTokenHandler = new JsonWebTokenHandler();

        string token = jsonWebTokenHandler.CreateToken(securityTokenDescriptor);

        return token;
    }
}
