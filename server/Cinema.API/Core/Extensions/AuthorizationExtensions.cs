using Cinema.API.Core.Constants;
using Cinema.Application.Features.Reservations.Policies;
using Cinema.Application.Shared.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.API.Core.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddCustomPolicyHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, ManageReservationHandler>();

        return services;
    }
    
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ManageCinema.ToString(),
                policy => policy.RequireRole(Roles.Manager.ToString().ToLower()));
            options.AddPolicy(Policies.ManageReservations.ToString(),
                policy =>
                {
                    policy.Requirements.Add(new ManageReservationRequirement());
                });
        });

        return services;
    }
}