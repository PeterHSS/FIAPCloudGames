using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login",
            async (LoginRequest request, LoginUseCase useCase) =>
            {
                string result = await useCase.HandleAsync(request);

                return Results.Ok(new { token = result });
            })
            .WithTags(Tags.Users)
            .AllowAnonymous();
    }
}
