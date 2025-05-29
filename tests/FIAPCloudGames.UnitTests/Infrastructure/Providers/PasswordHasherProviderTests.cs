using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Infrastructure.Providers;

namespace FIAPCloudGames.UnitTests.Infrastructure.Providers;

public class PasswordHasherProviderTests
{
    private readonly IPasswordHasherProvider _sut;

    public PasswordHasherProviderTests()
    {
        _sut = new PasswordHasherProvider();
    }

    [Fact]
    public void Hash_WithValidPassword_ShouldReturnNonNullAndDifferentString()
    {
        string password = "MinhaSenhaSegura123!";

        string hashed = _sut.Hash(password);

        Assert.NotNull(hashed);

        Assert.NotEmpty(hashed);

        Assert.NotEqual(password, hashed);

        Assert.Contains("-", hashed); // verifica formato esperado
    }

    [Fact]
    public void Verify_WithCorrectPassword_ShouldReturnTrue()
    {
        string password = "MinhaSenhaSegura123!";

        string hashed = _sut.Hash(password);

        bool result = _sut.Verify(password, hashed);

        Assert.True(result);
    }

    [Fact]
    public void Verify_WithIncorrectPassword_ShouldReturnFalse()
    {
        string password = "MinhaSenhaSegura123!";

        string wrongPassword = "SenhaErrada456!";

        string hashed = _sut.Hash(password);

        bool result = _sut.Verify(wrongPassword, hashed);

        Assert.False(result);
    }

    [Fact]
    public void Verify_WithInvalidHashedPasswordFormat_ShouldThrowFormatException()
    {
        string invalidHashedPassword = "hashInvalidoSemSeparador";

        FormatException exception = Assert.Throws<FormatException>(() => _sut.Verify("qualquer", invalidHashedPassword));

        Assert.Equal("Password with invalid hash format.", exception.Message);
    }
}
