using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerSecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Security ??= new List<OpenApiSecurityRequirement>();

        operation.Security.Add(new OpenApiSecurityRequirement
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
                Array.Empty<string>()
            }
        });
    }
}