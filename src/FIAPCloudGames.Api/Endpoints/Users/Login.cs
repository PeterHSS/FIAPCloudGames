using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login",
            async (LoginRequest request, LoginUseCase useCase, CancellationToken cancellation) =>
            {
                LoginResponse response = await useCase.HandleAsync(request, cancellation);

                return Results.Ok(response);
            })
            .WithTags(Tags.Users)
            .AllowAnonymous();
    }
}
