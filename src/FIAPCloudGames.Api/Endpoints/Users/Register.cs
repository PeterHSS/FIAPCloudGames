using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (CreateUserRequest request, CreateUserUseCase useCase) =>
        {
            await useCase.HandleAsync(request);

            return Results.Created();
        })
        .WithTags(Tags.Users)
        .AllowAnonymous();
    }
}
