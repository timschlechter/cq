using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PathItem
    {
        [DataMember(Name = "$ref")]
        public string @ref;

        public Operation get;

        public Operation put;

        public Operation post;

        public Operation delete;

        public Operation options;

        public Operation head;

        public Operation patch;

        public IList<Parameter> parameters;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}