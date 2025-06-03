using System.Data;
using Bogus;
using FIAPCloudGames.Application.DTOs.Promotion;
using FIAPCloudGames.Application.UseCases.Promotions;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Moq;

namespace FIAPCloudGames.UnitTests.Application.UseCases.Promotions;

public class CreatePromotionUseCaseTests
{
    private readonly Mock<IPromotionRepository> _promotionRepositoryMock;
    private readonly Mock<IGameRepository> _gameRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreatePromotionUseCase _useCase;
    private readonly Faker<CreatePromotionRequest> _faker;
    private readonly Mock<IDbTransaction> _transactionMock;

    public CreatePromotionUseCaseTests()
    {
        _promotionRepositoryMock = new();

        _gameRepositoryMock = new();

        _unitOfWorkMock = new();

        _transactionMock = new Mock<IDbTransaction>();

        _unitOfWorkMock
            .Setup(u => u.BeginTransaction(It.IsAny<CancellationToken>()))
            .Returns(_transactionMock.Object);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync (It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _useCase = new CreatePromotionUseCase(_promotionRepositoryMock.Object, _gameRepositoryMock.Object, _unitOfWorkMock.Object);

        _faker = new Faker<CreatePromotionRequest>()
            .CustomInstantiator(f => new CreatePromotionRequest(
                Name: f.Commerce.ProductName(),
                StartDate: f.Date.Future(1, DateTime.Today),
                EndDate: f.Date.Future(2, DateTime.Today),
                DiscountPercentage: f.Random.Decimal(),
                Description: f.Lorem.Sentence(),
                GamesId: []));

    }

    [Fact]
    public async Task HandleAsync_WhenRequestIsValid_ShouldCreatePromotion()
    {
        CreatePromotionRequest request = _faker.Generate();

        Promotion? capturedPromotion = null;

        _promotionRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Promotion>(), It.IsAny<CancellationToken>()))
            .Callback<Promotion, CancellationToken>((promotion, _) => capturedPromotion = promotion)
            .Returns(Task.CompletedTask);

        await _useCase.HandleAsync(request);

        _transactionMock
            .Verify(t => t.Commit(), Times.Once);


        Assert.NotNull(capturedPromotion);

        Assert.Equal(request.Name, capturedPromotion.Name);

        Assert.Equal(request.StartDate, capturedPromotion.StartDate);

        Assert.Equal(request.EndDate, capturedPromotion.EndDate);

        Assert.Equal(request.DiscountPercentage, capturedPromotion.DiscountPercentage);

        Assert.Equal(request.Description, capturedPromotion.Description);
    }
}
