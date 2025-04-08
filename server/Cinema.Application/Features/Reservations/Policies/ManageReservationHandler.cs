using System.Security.Claims;
using Cinema.Application.Shared.Constants;
using Cinema.Domain.Features.Reservations.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Application.Features.Reservations.Policies;

public class ManageReservationHandler : AuthorizationHandler<ManageReservationRequirement, Reservation>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        ManageReservationRequirement requirement, 
        Reservation reservation)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine(context.User);
        if (context.User.IsInRole(Roles.Manager.ToString().ToLower()))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        // TODO add customerId to reservation entity and compare ids
        // if (context.User.IsInRole(Roles.Customer.ToString().ToLower())
        //     && reservation.CustomerId == userId
        //     )
        // {
        //     context.Succeed(requirement);
        // }
        
        return Task.CompletedTask;
    }
}