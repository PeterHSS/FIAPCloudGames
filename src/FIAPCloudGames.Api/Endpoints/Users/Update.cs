using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;
using FluentValidation;

namespace FIAPCloudGames.Api.Endpoints.Users;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}",
            async (Guid id, UpdateUserRequest request, UpdateUserUseCase useCase, IValidator<UpdateUserRequest> validator) =>
            {
                validator.ValidateAndThrow(request);

                await useCase.HandleAsync(id, request);

                return Results.Ok();
            })
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}
