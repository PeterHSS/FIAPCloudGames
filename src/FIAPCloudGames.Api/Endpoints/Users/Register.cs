namespace FIAPCloudGames.Api.Endpoints.Users;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async () =>
        {
            await Task.Delay(100);

            return Results.Ok();
        })
        .WithTags(Tags.Users); 
    }
}
