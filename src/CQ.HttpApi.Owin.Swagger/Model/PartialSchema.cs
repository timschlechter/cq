using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PartialSchema
    {
        public string type;

        public string format;

        public PartialSchema items;

        public string collectionFormat;

        public object @default;

        public int? maximum;

        public bool? exclusiveMaximum;

        public int? minimum;

        public bool? exclusiveMinimum;

        public int? maxLength;

        public int? minLength;

        public string pattern;

        public int? maxItems;

        public int? minItems;

        public bool? uniqueItems;

        public IList<object> @enum;

        public int? multipleOf;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
    }
}