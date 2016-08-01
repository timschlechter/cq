using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RootDocument
    {
        public readonly string swagger = "2.0";

        public string basePath;

        public IList<string> consumes;

        public IDictionary<string, Schema> definitions;

        public ExternalDocs externalDocs;

        public string host;

        public Info info;

        public IDictionary<string, Parameter> parameters;

        public IDictionary<string, PathItem> paths;

        public IList<string> produces;

        public IDictionary<string, Response> responses;

        public IList<string> schemes;

        public IList<IDictionary<string, IEnumerable<string>>> security;

        public IDictionary<string, SecurityScheme> securityDefinitions;

        public IList<Tag> tags;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}