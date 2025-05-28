using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace FIAPCloudGames.UnitTests.Application.UseCases.Users
{
    public class CreateUserUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IValidator<CreateUserRequest>> _validatorMock = new();
        private readonly Mock<IPasswordHasherProvider> _passwordHasherMock = new();

        private readonly CreateUserUseCase _useCase;

        public CreateUserUseCaseTests() 
            => _useCase = new CreateUserUseCase(_userRepositoryMock.Object, _passwordHasherMock.Object);

        [Fact]
        public async Task HandleAsync_ValidRequest_ValidatesHashesAndCreatesUser()
        {
            // Arrange
            var request = new CreateUserRequest(
                "João Silva",
                "joao@example.com",
                "senha123",
                "joaos",
                "12345678900",
                new DateTime(1990, 1, 1));

            _validatorMock
                .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _passwordHasherMock
                .Setup(p => p.Hash(request.Password))
                .Returns("hashedSenha");

            User? createdUser = null;

            _userRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Callback<User, CancellationToken>((user, _) => createdUser = user)
                .Returns(Task.CompletedTask);


            // Act
            await _useCase.HandleAsync(request);

            // Assert
            _validatorMock.Verify(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()), Times.Once);

            _passwordHasherMock.Verify(p => p.Hash(request.Password), Times.Once);

            Assert.NotNull(createdUser);
            Assert.Equal(request.Name, createdUser!.Name);
            Assert.Equal(request.Email, createdUser.Email);
            Assert.Equal("hashedSenha", createdUser.Password);
            Assert.Equal(request.Nickname, createdUser.Nickname);
            Assert.Equal(request.Document, createdUser.Document);
            Assert.Equal(request.BirthDate, createdUser.BirthDate);
        }

        [Fact]
        public async Task HandleAsync_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            CreateUserRequest request = new CreateUserRequest("", "", "", "", "", default);

            ValidationFailure[] failures = [new ValidationFailure("Name", "Name is required")];

            ValidationResult validationResult = new ValidationResult(failures);

            _validatorMock
                .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _useCase.HandleAsync(request));
        }
    }
}
