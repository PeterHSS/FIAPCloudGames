using Asp.Versioning;
using Asp.Versioning.Builder;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using FIAPCloudGames.Api;
using FIAPCloudGames.Api.Extensions;
using FIAPCloudGames.Api.Middlewares;
using FIAPCloudGames.Application;
using FIAPCloudGames.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Host.AddSerilog();

builder.Services
    .AddOpenTelemetry()
    .UseAzureMonitor(configureAzureMonitor => configureAzureMonitor.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"]);

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

    app.ApplyMigrations();
}

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseJwtAuthenticationAndAuthorization();

app.MapEndpoints(versionedGroup);

app.Run();