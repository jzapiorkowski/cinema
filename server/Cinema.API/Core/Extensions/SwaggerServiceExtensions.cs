using Cinema.API.Core.SchemaFilters;
using Microsoft.OpenApi.Models;

namespace Cinema.API.Core.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SchemaFilter<Iso8601TimeSpanSchemaFilter>();
            c.SchemaFilter<Iso8601DateOnlySchemaFilter>();
            c.DescribeAllParametersInCamelCase();
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer <your-token>'"
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static void UseSwaggerDocumentation(this WebApplication app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment()) return;
        
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
