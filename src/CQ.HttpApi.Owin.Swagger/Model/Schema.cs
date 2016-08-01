using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Schema
    {
        public Schema additionalProperties;

        public IList<Schema> allOf;

        public object @default;

        public string description;

        public string discriminator;

        public IList<object> @enum;

        public object example;

        public bool? exclusiveMaximum;

        public bool? exclusiveMinimum;

        public ExternalDocs externalDocs;

        public string format;

        public Schema items;

        public int? maximum;

        public int? maxItems;

        public int? maxLength;

        public int? maxProperties;

        public int? minimum;

        public int? minItems;

        public int? minLength;

        public int? minProperties;

        public int? multipleOf;

        public string pattern;

        public IDictionary<string, Schema> properties;

        public bool? readOnly;

        [DataMember(Name = "$ref")] public string @ref;

        public IList<string> required;

        public string title;

        public string type;

        public bool? uniqueItems;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();

        public Xml xml;
    }
}