using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PartialSchema
    {
        public string collectionFormat;

        public object @default;

        public IList<object> @enum;

        public bool? exclusiveMaximum;

        public bool? exclusiveMinimum;

        public string format;

        public PartialSchema items;

        public int? maximum;

        public int? maxItems;

        public int? maxLength;

        public int? minimum;

        public int? minItems;

        public int? minLength;

        public int? multipleOf;

        public string pattern;
        public string type;

        public bool? uniqueItems;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}