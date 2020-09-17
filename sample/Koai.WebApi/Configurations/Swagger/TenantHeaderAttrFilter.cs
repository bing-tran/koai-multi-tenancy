using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Koai.WebApi.Configurations.Swagger
{
    public class TenantHeaderAttrFilter : IOperationFilter
    {
        public TenantHeaderAttrFilter()
        {
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-tenant",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = nameof(OpenApiInteger),
                },
            });
        }
    }
}
