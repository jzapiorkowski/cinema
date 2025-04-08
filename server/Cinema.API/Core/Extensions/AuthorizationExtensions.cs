using Cinema.API.Core.Constants;
using Cinema.API.Features.Reservations.Policies;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.API.Core.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddCustomPolicyHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, ManageReservationHandler>();
        services.AddSingleton<IAuthorizationHandler, CreateReservationHandler>();

        return services;
    }

    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ManageCinema,
                policy => policy.RequireRole(Roles.Manager.ToLower()));
            options.AddPolicy(Policies.ManageReservations,
                policy => { policy.Requirements.Add(new ManageReservationRequirement()); });
            options.AddPolicy(Policies.CreateReservation,
                policy => { policy.Requirements.Add(new CreateReservationRequirement()); });
        });

        return services;
    }
}