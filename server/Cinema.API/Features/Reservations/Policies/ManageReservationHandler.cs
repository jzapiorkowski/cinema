using Cinema.API.Core.Extensions;
using Cinema.Application.Features.Reservations.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.API.Features.Reservations.Policies;

public class ManageReservationHandler : AuthorizationHandler<ManageReservationRequirement, ReservationAppResponseDto>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ManageReservationRequirement requirement,
        ReservationAppResponseDto reservation)
    {
        if (context.User.IsManager())
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (
            context.User.IsCustomer()
            && reservation.CustomerId == context.User.GetUserId()
        )
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}