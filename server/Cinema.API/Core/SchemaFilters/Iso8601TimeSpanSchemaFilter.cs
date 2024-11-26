using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cinema.API.Core.SchemaFilters;

public class Iso8601TimeSpanSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(TimeSpan))
        {
            schema.Type = "string";
            schema.Format = "duration";
            schema.Example = new OpenApiString("PT2H35M10S");
            schema.Properties.Clear();
            schema.Description = "Duration in ISO 8601 format";
        }
    }
}