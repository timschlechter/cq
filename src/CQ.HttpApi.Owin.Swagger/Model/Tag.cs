using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Tag
    {
        public string name;

        public string description;

        public ExternalDocs externalDocs;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}