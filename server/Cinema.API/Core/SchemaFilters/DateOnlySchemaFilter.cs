using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cinema.API.Core.SchemaFilters;

public class DateOnlySchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(DateTime))
        {
            schema.Format = "date";
            schema.Example = new OpenApiString("yyyy-MM-dd");
        }
    }
}