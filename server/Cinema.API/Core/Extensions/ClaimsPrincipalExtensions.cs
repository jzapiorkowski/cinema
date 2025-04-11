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
}