using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SecurityScheme
    {
        public string authorizationUrl;

        public string description;

        public string flow;

        public string @in;

        public string name;

        public IDictionary<string, string> scopes;

        public string tokenUrl;
        public string type;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}