using System.Text.Json.Serialization;
using Cinema.API.Core.Converters;
using Cinema.API.Core.Middlewares;
using Cinema.API.Core.SchemaFilters;
using Cinema.Application;
using Cinema.Infrastructure;

namespace Cinema.API;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SchemaFilter<Iso8601TimeSpanSchemaFilter>();
            c.SchemaFilter<Iso8601DateOnlySchemaFilter>();
            c.DescribeAllParametersInCamelCase();
        });

        services.AddInfrastructureServices();
        services.AddAPIServices();
        services.AddApplicationServices();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new Iso8601TimeSpanConverter());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddControllers(
            options => { options.SuppressAsyncSuffixInActionNames = false; }
        );
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