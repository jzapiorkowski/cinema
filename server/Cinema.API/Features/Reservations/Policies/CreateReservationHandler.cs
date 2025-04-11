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
        var userId = context.User.GetUserId();

        if (context.User.IsCustomer() && (dto.CustomerId == null || dto.CustomerId == userId))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (dto.CustomerId != null && context.User.IsManager())
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}