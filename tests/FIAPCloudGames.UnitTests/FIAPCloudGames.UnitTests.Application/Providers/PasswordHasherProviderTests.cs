using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Infrastructure.Providers;

namespace FIAPCloudGames.Infrastructure.Tests.Providers
{
    public class PasswordHasherProviderTests
    {
        private readonly IPasswordHasherProvider _sut;

        public PasswordHasherProviderTests()
        {
            _sut = new PasswordHasherProvider();
        }

        [Fact]
        public void Hash_WithValidPassword_ReturnsNonNullAndDifferentString()
        {
            // Arrange
            string password = "MinhaSenhaSegura123!";

            // Act
            string hashed = _sut.Hash(password);

            // Assert
            Assert.NotNull(hashed);
            
            Assert.NotEmpty(hashed);
            
            Assert.NotEqual(password, hashed);
            
            Assert.Contains("-", hashed); // verifica formato esperado
        }

        [Fact]
        public void Verify_WithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            string password = "MinhaSenhaSegura123!";
            
            string hashed = _sut.Hash(password);

            // Act
            bool result = _sut.Verify(password, hashed);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Verify_WithIncorrectPassword_ReturnsFalse()
        {
            // Arrange
            string password = "MinhaSenhaSegura123!";
            
            string wrongPassword = "SenhaErrada456!";
            
            string hashed = _sut.Hash(password);

            // Act
            bool result = _sut.Verify(wrongPassword, hashed);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Verify_WithInvalidHashedPasswordFormat_ThrowsFormatException()
        {
            // Arrange
            string invalidHashedPassword = "hashInvalidoSemSeparador";

            // Act & Assert
            FormatException exception = Assert.Throws<FormatException>(() => _sut.Verify("qualquer", invalidHashedPassword));
            
            Assert.Equal("Password with invalid hash format.", exception.Message);
        }
    }
}
