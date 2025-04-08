using System.Text.Json;
using System.Text.Json.Serialization;
using Cinema.API.Core.Constants;
using Cinema.API.Core.Converters;
using Cinema.API.Core.Extensions;
using Cinema.API.Core.Middlewares;
using Cinema.Application;
using Cinema.Infrastructure;

namespace Cinema.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        // register settings
        var keycloakOptions = _configuration.GetSection("keycloak");
        services.Configure<KeycloakSettings>(keycloakOptions);

        // register authentication and authorization services
        services.AddJwtAuthentication(keycloakOptions.Get<KeycloakSettings>());
        services.AddCustomPolicyHandlers();
        services.AddCustomAuthorization();

        // register swagger services
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        // register services from other layers 
        services.AddInfrastructureServices();
        services.AddAPIServices();
        services.AddApplicationServices();

        // register json options
        services.ConfigureHttpJsonOptions(options => ConfigureJsonSerializerOptions(options.SerializerOptions));

        // register controllers
        services.AddControllers(
                options => { options.SuppressAsyncSuffixInActionNames = false; }
            )
            .AddJsonOptions(options => ConfigureJsonSerializerOptions(options.JsonSerializerOptions));
    }

    public static void ConfigureMiddleware(WebApplication app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();

        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseSwaggerDocumentation(env);

        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void ConfigureEndpoints(WebApplication app)
    {
        app.MapControllers().RequireAuthorization();
    }

    private static void ConfigureJsonSerializerOptions(JsonSerializerOptions options)
    {
        options.Converters.Add(new Iso8601TimeSpanConverter());
        options.Converters.Add(new JsonStringEnumConverter());
    }
}