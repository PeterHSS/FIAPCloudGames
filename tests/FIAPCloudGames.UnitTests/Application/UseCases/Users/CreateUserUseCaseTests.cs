using Bogus;
using Bogus.Extensions.Brazil;
using FIAPCloudGames.Application.Abstractions.Infrastructure.Providers;
using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.Helpers;
using FIAPCloudGames.Application.UseCases.Users;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Moq;

namespace FIAPCloudGames.UnitTests.Application.UseCases.Users;

public class CreateUserUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasherProvider> _passwordHasherMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateUserUseCase _useCase;
    private readonly Faker<CreateUserRequest> _faker;

    public CreateUserUseCaseTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();

        _passwordHasherMock = new Mock<IPasswordHasherProvider>();

        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _useCase = new CreateUserUseCase(_userRepositoryMock.Object, _passwordHasherMock.Object, _unitOfWorkMock.Object);
        
        _faker = new Faker<CreateUserRequest>()
            .CustomInstantiator(f => new CreateUserRequest(
                f.Person.FullName,
                f.Internet.Email(),
                f.Internet.Password(),
                f.Internet.UserName(),
                f.Person.Cpf(true),
                f.Date.Past(30, DateTime.Today.AddYears(-18))));
    }      

    [Fact]
    public async Task HandleAsync_ShouldHashPasswordAndAddUserToRepository()
    {
        CreateUserRequest request = _faker.Generate();

        string hashedPassword = "hashedPassword";

        _passwordHasherMock
            .Setup(p => p.Hash(request.Password))
            .Returns(hashedPassword);

        User? capturedUser = null;

        _userRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback<User, CancellationToken>((user, _) => capturedUser = user)
            .Returns(Task.CompletedTask);
        
        await _useCase.HandleAsync(request);

        _passwordHasherMock.Verify(p => p.Hash(request.Password), Times.Once);

        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotNull(capturedUser);
        
        Assert.Equal(request.Name, capturedUser!.Name);
        
        Assert.Equal(request.Email, capturedUser.Email);
        
        Assert.Equal(hashedPassword, capturedUser.Password);
        
        Assert.Equal(request.Nickname, capturedUser.Nickname);
        
        Assert.Equal(request.Document.OnlyNumbers(), capturedUser.Document); // OnlyNumbers() removes punctuation
        
        Assert.Equal(request.BirthDate, capturedUser.BirthDate);
    }
}
