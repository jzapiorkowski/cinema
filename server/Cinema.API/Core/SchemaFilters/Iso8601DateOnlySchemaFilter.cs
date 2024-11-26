using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cinema.API.Core.SchemaFilters;

public class Iso8601DateOnlySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(DateOnly))
        {
            schema.Type = "string";
            schema.Format = "date";
            schema.Example = new OpenApiString("2021-12-31");
            schema.Properties.Clear();
            schema.Description = "Date in ISO 8601 format";
        }
    }
}