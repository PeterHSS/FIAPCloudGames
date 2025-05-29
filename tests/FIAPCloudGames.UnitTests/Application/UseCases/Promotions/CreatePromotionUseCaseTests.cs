using FIAPCloudGames.Application.DTOs.Promotion;
using FIAPCloudGames.Domain.Abstractions.Repositories;
using FIAPCloudGames.Domain.Entities;
using Moq;

namespace FIAPCloudGames.UnitTests.Application.UseCases.Promotions;

public class CreatePromotionUseCaseTests
{
    [Fact]
    public async Task HandleAsync_WhenRequestIsValid_ShouldCreatePromotion()
    {
        // Arrange
        CreatePromotionRequest request = new()
        {
            Name = "Natal",
            StartDate = new DateTime(2025, 12, 15),
            EndDate = new DateTime(2025, 12, 31),
            DiscountPercentage = 20,
            Description = "Promoção de Natal"
        };

        Promotion promotion = Promotion.Create(
            name: request.Name,
            startDate: request.StartDate,
            endDate: request.EndDate,
            discountPercentage: request.DiscountPercentage,
            description: request.Description);

        Mock<IPromotionRepository> promotionRepository = new Mock<IPromotionRepository>();

        promotionRepository
            .Setup(r => r.AddAsync(It.IsAny<Promotion>()))
            .ReturnsAsync((Promotion p) => p);

        var useCase = new CreatePromotionUseCase(promotionRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        promotionRepository.Verify(r => r.AddAsync(It.IsAny<Users>()), Times.Once);
    }
}
