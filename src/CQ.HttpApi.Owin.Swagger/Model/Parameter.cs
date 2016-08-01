using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Parameter : PartialSchema
    {
        public string description;

        public string @in;

        public string name;

        [DataMember(Name = "$ref")] public string @ref;

        public bool? required;

        public Schema schema;
    }
}