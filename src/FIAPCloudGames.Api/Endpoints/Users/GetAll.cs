using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (GetAllUsersUseCase useCase) =>
        {
            IEnumerable<UserResponse> response = await useCase.HandleAsync();

            return Results.Ok(response);
        })
        .WithTags(Tags.Users);
    }
}
