using Cinema.API.Core.Constants;

namespace Cinema.API.Core.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ManageCinema.ToString(),
                policy => policy.RequireRole(Roles.Manger.ToString().ToLower()));
        });

        return services;
    }
}