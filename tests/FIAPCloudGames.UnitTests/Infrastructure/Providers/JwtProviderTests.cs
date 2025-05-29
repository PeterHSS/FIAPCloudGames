using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FIAPCloudGames.Domain.Entities;
using FIAPCloudGames.Domain.Enums;
using FIAPCloudGames.Infrastructure.Providers;
using FIAPCloudGames.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;

namespace FIAPCloudGames.UnitTests.Infrastructure.Providers;

public class JwtProviderTests
{
    private readonly JwtSettings _jwtSettings = new()
    {
        Secret = "MinhaSuperSecretaChaveParaJWT1234567890!",
        ExpirationInMinutes = 60,
        Issuer = "FIAPCloudGames",
        Audience = "FIAPCloudGamesUsers"
    };

    private readonly JwtProvider _sut;

    public JwtProviderTests()
    {
        _sut = new JwtProvider(_jwtSettings);
    }

    [Fact]
    public void Create_WithValidUser_ShouldReturnToken()
    {
        User user = User.Create(
            name: "Teste",
            email: "teste@fiap.com",
            password: "hashedpassword",
            nickname: "nick",
            document: "123456789",
            birthDate: DateTime.Parse("1990-01-01"));

        string token = _sut.Create(user);

        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    [Fact]
    public void Create_WithValidUser_ShouldContainExpectedRoleClaim()
    {
        string email = "user@example.com";

        RoleEnum role = RoleEnum.User;

        User user = User.Create(
            name: "User Test",
            email: email,
            password: "hashed",
            nickname: "nickname",
            document: "doc",
            birthDate: DateTime.UtcNow.AddYears(-30));

        string tokenString = _sut.Create(user);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken token = tokenHandler.ReadJwtToken(tokenString);

        string? roleClaimValue = token.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

        Assert.Equal(role.ToString(), roleClaimValue);
    }

    [Fact]
    public void Create_WithValidUser_ShouldHaveCorrectExpiration()
    {
        // Arrange
        User user = User.Create(
            name: "Teste",
            email: "teste@fiap.com",
            password: "hashedpassword",
            nickname: "nick",
            document: "123456789",
            birthDate: DateTime.Parse("1990-01-01"));

        string tokenString = _sut.Create(user);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken token = tokenHandler.ReadJwtToken(tokenString);

        DateTime expectedExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);

        Assert.InRange(token.ValidTo, DateTime.UtcNow, expectedExpiration.AddMinutes(1));
    }

    [Fact]
    public void Create_WithValidUser_ShouldBeValidWithCorrectValidationParameters()
    {
        User user = User.Create("Teste", "teste@fiap.com", "hashedpassword", "nick", "123456789", DateTime.Parse("1990-01-01"));

        string tokenString = _sut.Create(user);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        byte[] key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        tokenHandler.ValidateToken(tokenString, validationParameters, out SecurityToken? validatedToken);

        Assert.NotNull(validatedToken);
    }
}
