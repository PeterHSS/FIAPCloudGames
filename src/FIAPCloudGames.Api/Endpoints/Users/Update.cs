using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;
using FluentValidation;
using FluentValidation.Results;

namespace FIAPCloudGames.Api.Endpoints.Users;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}", async (Guid id, UpdateUserRequest request, UpdateUserUseCase useCase, IValidator<UpdateUserRequest> validator) =>
        {
            ValidationResult validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            await useCase.HandleAsync(id, request);

            return Results.Ok();
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
