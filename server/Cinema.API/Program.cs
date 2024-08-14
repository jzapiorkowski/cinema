using Cinema.API;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup();
startup.ConfigureServices(builder.Services);

var app = builder.Build();

Startup.ConfigureMiddleware(app, builder.Environment);
Startup.ConfigureEndpoints(app);

app.Run();