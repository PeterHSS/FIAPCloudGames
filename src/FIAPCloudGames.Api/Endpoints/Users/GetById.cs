using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{id:guid}", async (Guid id, GetUserByIdUseCase useCase) =>
        {
            UserResponse response = await useCase.HandleAsync(id);

            return Results.Ok(response);
        })
        .WithTags(Tags.Users); 
    }
}
