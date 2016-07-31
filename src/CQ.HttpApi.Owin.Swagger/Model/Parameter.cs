using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Parameter : PartialSchema
    {
        [DataMember(Name = "$ref")]
        public string @ref;

        public string name;

        public string @in;

        public string description;

        public bool? required;

        public Schema schema;
    }
}