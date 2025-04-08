using System.Security.Claims;
using Cinema.API.Core.Constants;
using Cinema.Application.Features.Reservations.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.API.Features.Reservations.Policies;

public class ManageReservationHandler : AuthorizationHandler<ManageReservationRequirement, ReservationAppResponseDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ManageReservationRequirement requirement,
        ReservationAppResponseDto reservation)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (context.User.IsInRole(Roles.Manager.ToLower()))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (
            context.User.IsInRole(Roles.Customer.ToLower())
            && reservation.CustomerId.ToString() == userId
        )
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}