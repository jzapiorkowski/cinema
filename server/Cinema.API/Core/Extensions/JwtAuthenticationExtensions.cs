using System.Security.Claims;
using System.Text.Json;
using Cinema.API.Core.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.API.Core.Extensions;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        KeycloakSettings keycloakSettings)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = keycloakSettings.Authority;
                options.Audience = keycloakSettings.Audience;
                options.RequireHttpsMetadata = Convert.ToBoolean(keycloakSettings.RequireHttpsMetadata);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = AuthenticationFailed,
                    OnTokenValidated = TokenValidated,
                    OnChallenge = Challenge
                };
            });

        return services;
    }

    private static Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
        return Task.CompletedTask;
    }

    private static Task Challenge(JwtBearerChallengeContext context)
    {
        Console.WriteLine($"Authentication challenge: {context.Error}, {context.ErrorDescription}");
        return Task.CompletedTask;
    }

    private static Task TokenValidated(TokenValidatedContext context)
    {
        if (context.Principal?.Identity is not ClaimsIdentity claimsIdentity)
        {
            return Task.CompletedTask;
        }

        var realmAccessClaim = context.Principal.FindFirst(CustomClaimTypes.RealmAccess)?.Value;
        if (string.IsNullOrEmpty(realmAccessClaim))
        {
            return Task.CompletedTask;
        }

        try
        {
            using var json = JsonDocument.Parse(realmAccessClaim);
            if (json.RootElement.TryGetProperty("roles", out var rolesElement) &&
                rolesElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var roleElement in rolesElement.EnumerateArray())
                {
                    var role = roleElement.GetString();
                    if (!string.IsNullOrEmpty(role) && Enum.TryParse(typeof(Roles), role, true, out _))
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                    else
                    {
                        Console.WriteLine($"Role {role} is not supported");
                    }
                }
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error parsing realm roles: {ex.Message}");
        }

        Console.WriteLine("Token validated successfully");
        return Task.CompletedTask;
    }
}