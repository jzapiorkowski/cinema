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
            c.SchemaFilter<TimeSpanSchemaFilter>();
            c.SchemaFilter<DateOnlySchemaFilter>();
        });

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