using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Swashbuckle.Swagger;

namespace Samples.WebApi.Code
{
    public class CustomSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            foreach (var kvp in schema.properties)
            {
                var propertyInfo = type.GetProperties()
                    .FirstOrDefault(pi => pi.GetCustomAttribute<DataMemberAttribute>()?.Name == kvp.Key);

                if (propertyInfo != null)
                {
                    kvp.Value.description = kvp.Value.description ?? propertyInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;
                }
            }
        }
    }
}