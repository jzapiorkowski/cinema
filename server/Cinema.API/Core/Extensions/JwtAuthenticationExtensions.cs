using System.Security.Claims;
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

        Console.WriteLine("Token validated successfully");
        return Task.CompletedTask;
    }
}