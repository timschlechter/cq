using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Operation
    {
        public IList<string> consumes;

        public bool deprecated;

        public string description;

        public ExternalDocs externalDocs;

        public string operationId;

        public IList<Parameter> parameters;

        public IList<string> produces;

        public IDictionary<string, Response> responses;

        public IList<string> schemes;

        public IList<IDictionary<string, IEnumerable<string>>> security;

        public string summary;
        public IList<string> tags;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}