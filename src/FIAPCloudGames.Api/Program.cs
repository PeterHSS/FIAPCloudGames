using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using FIAPCloudGames.Api;
using FIAPCloudGames.Api.Extensions;
using FIAPCloudGames.Application;
using FIAPCloudGames.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app.MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.MapEndpoints(versionedGroup);

app.UseHttpsRedirection();

app.Run();