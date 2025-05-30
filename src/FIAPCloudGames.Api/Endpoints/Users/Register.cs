using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;
using FluentValidation;

namespace FIAPCloudGames.Api.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (CreateUserRequest request, CreateUserUseCase useCase, IValidator<CreateUserRequest> validator) =>
        {
            validator.ValidateAndThrow(request);
            
            await useCase.HandleAsync(request);

            return Results.Created();
        })
        .WithTags(Tags.Users)
        .AllowAnonymous();
    }
}
