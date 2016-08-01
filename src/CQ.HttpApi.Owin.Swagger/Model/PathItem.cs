using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PathItem
    {
        public Operation delete;

        public Operation get;

        public Operation head;

        public Operation options;

        public IList<Parameter> parameters;

        public Operation patch;

        public Operation post;

        public Operation put;

        [DataMember(Name = "$ref")] public string @ref;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}