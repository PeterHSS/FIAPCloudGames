﻿using FIAPCloudGames.Application.DTOs.Users;
using FIAPCloudGames.Application.UseCases.Users;

namespace FIAPCloudGames.Api.Endpoints.Users;

public sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (LoginRequest request, LoginUseCase useCase) =>
        {
            string result = await useCase.HandleAsync(request);

            return Results.Ok(result);
        })
        .WithTags(Tags.Users)
        .AllowAnonymous();
    }
}
