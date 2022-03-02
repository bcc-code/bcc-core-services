using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BuildingBlocks.Api.OpenApi
{
    public class TestHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "AW-User-Id",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema {Type = "number"},
                Required = true
            });
        }
    }
}