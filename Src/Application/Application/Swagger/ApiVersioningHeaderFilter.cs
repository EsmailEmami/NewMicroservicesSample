using Application.ApiVersioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Application.Swagger;

public class ApiVersioningHeaderFilter : IOperationFilter
{
    private readonly string[] _values;
    public ApiVersioningHeaderFilter(IOptions<ApiVersioningOptions> options)
    {
        _values = options.Value.VersionHeaderReader;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var value in _values)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = value,
                In = ParameterLocation.Header,
                Required = true,
                Example = new OpenApiString("1.0")
            });
        }
    }
}