using Cinema.API.Core.Converters;
using Cinema.API.Core.Middlewares;
using Cinema.Application;
using Cinema.Infrastructure;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Cinema.API;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.MapType<TimeSpan>(() => new OpenApiSchema
        {
            Type = "string",
            Example = new OpenApiString("02:00:00")
        }));

        services.AddInfrastructureServices();
        services.AddAPIServices();
        services.AddApplicationServices();

        services.AddControllers(
                options => { options.SuppressAsyncSuffixInActionNames = false; }
            )
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter()); });
    }

    public static void ConfigureMiddleware(WebApplication app, IWebHostEnvironment env)
    {
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ErrorHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
    }

    public static void ConfigureEndpoints(WebApplication app)
    {
        app.MapControllers();
    }
}