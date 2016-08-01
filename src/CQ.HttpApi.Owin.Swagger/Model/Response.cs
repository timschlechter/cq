using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Response
    {
        public string description;

        public object examples;

        public IDictionary<string, Header> headers;

        public Schema schema;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}