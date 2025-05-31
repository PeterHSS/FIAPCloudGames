using Bogus;
using FIAPCloudGames.Application.DTOs.Promotion;
using FIAPCloudGames.Application.Validators.Promotion;
using FluentValidation.TestHelper;

namespace FIAPCloudGames.UnitTests.Application.Validators;

public class CreatePromotionValidatorTests
{
    private readonly CreatePromotionValidator _validator;
    private readonly Faker<CreatePromotionRequest> _faker;

    public CreatePromotionValidatorTests()
    {
        _validator = new CreatePromotionValidator();

        _faker = new Faker<CreatePromotionRequest>()
            .CustomInstantiator(f => new CreatePromotionRequest(
                f.Lorem.Letter(50),
                f.Date.Past(1, DateTime.Today),
                f.Date.Future(1, DateTime.Today),
                f.Random.Decimal(0, 100),
                f.Lorem.Sentence(10),
                []));
    }

    [Fact]
    public void Validate_NameExceedsMaxLength_ShouldHaveValidationError()
    {
        CreatePromotionRequest createPromotionRequest = _faker.Generate();

        createPromotionRequest = createPromotionRequest with { Name = new string('A', 501) };

        TestValidationResult<CreatePromotionRequest> result = _validator.TestValidate(createPromotionRequest);

        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    [Fact]
    public void Validate_StartDateIsDefault_ShouldHaveValidationError()
    {
        CreatePromotionRequest createPromotionRequest = _faker.Generate();

        createPromotionRequest = createPromotionRequest with { StartDate = default };

        TestValidationResult<CreatePromotionRequest> result = _validator.TestValidate(createPromotionRequest);

        result.ShouldHaveValidationErrorFor(p => p.StartDate);
    }

    [Fact]
    public void Validate_EndDateBeforeStartDate_ShouldHaveValidationError()
    {
        CreatePromotionRequest createPromotionRequest = _faker.Generate();

        createPromotionRequest = createPromotionRequest with { EndDate = createPromotionRequest.StartDate.AddDays(-1) };

        TestValidationResult<CreatePromotionRequest> testValidationResult = _validator.TestValidate(createPromotionRequest);

        testValidationResult.ShouldHaveValidationErrorFor(p => p.EndDate);
    }

    [Fact]
    public void Validate_DiscountPercentageOutOfRange_ShouldHaveValidationError()
    {
        CreatePromotionRequest createPromotionRequest = _faker.Generate();

        createPromotionRequest = createPromotionRequest with { DiscountPercentage = 101m };

        TestValidationResult<CreatePromotionRequest> testValidationResult = _validator.TestValidate(createPromotionRequest);

        testValidationResult.ShouldHaveValidationErrorFor(p => p.DiscountPercentage);
    }

    [Fact]
    public void Validate_DescriptionExceedsMaxLength_ShouldHaveValidationError()
    {
        CreatePromotionRequest createPromotionRequest = _faker.Generate();

        createPromotionRequest = createPromotionRequest with { Description = new string('A', 1001) };

        TestValidationResult<CreatePromotionRequest> testValidationResult = _validator.TestValidate(createPromotionRequest);

        testValidationResult.ShouldHaveValidationErrorFor(p => p.Description);
    }

    [Fact]
    public void Validate_ValidPromotion_ShouldNotHaveAnyValidationErrors()
    {
        CreatePromotionRequest createPromotionRequest = _faker.Generate();

        TestValidationResult<CreatePromotionRequest> result = _validator.TestValidate(createPromotionRequest);
        
        result.ShouldNotHaveAnyValidationErrors();
    }
}
