using System.Security.Claims;
using Cinema.API.Core.Constants;

namespace Cinema.API.Core.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal principal)
    {
        ArgumentNullException.ThrowIfNull(principal);

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

        return Guid.Parse(claim?.Value);
    }

    public static bool IsCustomer(this ClaimsPrincipal principal)
        => principal.IsInRole(Roles.Customer);

    public static bool IsManager(this ClaimsPrincipal principal)
        => principal.IsInRole(Roles.Manager);
}