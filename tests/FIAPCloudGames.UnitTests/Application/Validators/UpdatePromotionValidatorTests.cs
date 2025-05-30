using Bogus;
using FIAPCloudGames.Application.DTOs.Promotions;
using FIAPCloudGames.Application.Validators.Promotion;
using FluentValidation.TestHelper;

namespace FIAPCloudGames.UnitTests.Application.Validators;

public class UpdatePromotionValidatorTests
{
    private readonly UpdatePromotionValidator _validator;
    private readonly Faker<UpdatePromotionRequest> _faker;

    public UpdatePromotionValidatorTests()
    {
        _validator = new UpdatePromotionValidator ();

        _faker = new Faker<UpdatePromotionRequest>()
            .CustomInstantiator(f => new UpdatePromotionRequest(
                f.Lorem.Letter(50),
                f.Date.Past(1, DateTime.Today),
                f.Date.Future(1, DateTime.Today),
                f.Random.Decimal(0, 100),
                f.Lorem.Sentence(10)));
    }

    [Fact]
    public void Validate_NameExceedsMaxLength_ShouldHaveValidationError()
    {
        UpdatePromotionRequest request = _faker.Generate();

        request = request with { Name = new string('A', 501) };

        TestValidationResult<UpdatePromotionRequest> result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    [Fact]
    public void Validate_StartDateIsDefault_ShouldHaveValidationError()
    {
        UpdatePromotionRequest request = _faker.Generate();

        request = request with { StartDate = default };

        TestValidationResult<UpdatePromotionRequest> result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(p => p.StartDate);
    }

    [Fact]
    public void Validate_EndDateBeforeStartDate_ShouldHaveValidationError()
    {
        UpdatePromotionRequest request = _faker.Generate();

        request = request with { EndDate = request.StartDate.AddDays(-1) };

        TestValidationResult<UpdatePromotionRequest> testValidationResult = _validator.TestValidate(request);

        testValidationResult.ShouldHaveValidationErrorFor(p => p.EndDate);
    }

    [Fact]
    public void Validate_DiscountPercentageOutOfRange_ShouldHaveValidationError()
    {
        UpdatePromotionRequest request = _faker.Generate();

        request = request with { DiscountPercentage = 101m };

        TestValidationResult<UpdatePromotionRequest> testValidationResult = _validator.TestValidate(request);

        testValidationResult.ShouldHaveValidationErrorFor(p => p.DiscountPercentage);
    }

    [Fact]
    public void Validate_DescriptionExceedsMaxLength_ShouldHaveValidationError()
    {
        UpdatePromotionRequest request = _faker.Generate();

        request = request with { Description = new string('A', 1001) };

        TestValidationResult<UpdatePromotionRequest> testValidationResult = _validator.TestValidate(request);

        testValidationResult.ShouldHaveValidationErrorFor(p => p.Description);
    }

    [Fact]
    public void Validate_ValidPromotion_ShouldNotHaveAnyValidationErrors()
    {
        UpdatePromotionRequest request = _faker.Generate();

        TestValidationResult<UpdatePromotionRequest> result = _validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
