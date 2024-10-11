var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
