using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{id:guid}", async (Guid id, UpdateUserRequest request, UpdateUserUseCase useCase) =>
        {
            await useCase.HandleAsync(id, request);

            return Results.Ok();
        })
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}
