using Microsoft.AspNetCore.RateLimiting;
using BuildingBlocks.Observability;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAppObservability(builder.Configuration);

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

var app = builder.Build();

// Map observability endpoints
app.MapAppObservability();

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
