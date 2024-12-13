using Cinema.API;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

var startup = new Startup();
startup.ConfigureServices(builder.Services);

var app = builder.Build();

Startup.ConfigureMiddleware(app, builder.Environment);
Startup.ConfigureEndpoints(app);

app.Run();