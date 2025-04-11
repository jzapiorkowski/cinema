using Cinema.API.Core.Constants;
using Cinema.API.Core.Extensions;
using Cinema.API.Features.Reservations.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.API.Features.Reservations.Policies;

public class CreateReservationHandler : AuthorizationHandler<CreateReservationRequirement, CreateReservationApiDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CreateReservationRequirement requirement,
        CreateReservationApiDto dto)
    {
        var isManager = context.User.IsInRole(Roles.Manager.ToLower());
        var isCustomer = context.User.IsInRole(Roles.Customer.ToLower());
        var userId = context.User.GetUserId();

        if (isCustomer && (dto.CustomerId == null || dto.CustomerId == userId))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (dto.CustomerId != null && isManager)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}