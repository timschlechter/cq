using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace Samples.WebApi.Code
{
    public class CustomOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            operation.summary = operation.summary ?? apiDescription.Documentation;
        }
    }
}