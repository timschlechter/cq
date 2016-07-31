using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RootDocument
    {
        public readonly string swagger = "2.0";

        public Info info;

        public string host;

        public string basePath;

        public IList<string> schemes;

        public IList<string> consumes;

        public IList<string> produces;

        public IDictionary<string, PathItem> paths;

        public IDictionary<string, Schema> definitions;

        public IDictionary<string, Parameter> parameters;

        public IDictionary<string, Response> responses;

        public IDictionary<string, SecurityScheme> securityDefinitions;

        public IList<IDictionary<string, IEnumerable<string>>> security;

        public IList<Tag> tags;

        public ExternalDocs externalDocs;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}