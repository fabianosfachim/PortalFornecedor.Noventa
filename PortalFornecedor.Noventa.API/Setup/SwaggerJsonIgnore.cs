using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PortalFornecedor.Noventa.API.Setup
{
    public class SwaggerJsonIgnore : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var ignoredProperties = context.MethodInfo.GetParameters()
                .SelectMany(p => p.ParameterType.GetProperties()
                                 .Where(prop => prop.CustomAttributes.Any(a => a.AttributeType.Name == nameof(JsonIgnoreAttribute))));

            if (ignoredProperties.Any())
            {
                foreach (var property in ignoredProperties)
                {
                    operation.Parameters = operation.Parameters
                        .Where(p => (!p.Name.Equals(property.Name, StringComparison.InvariantCulture)))
                        .ToList();
                }

            }
        }

    }
}
