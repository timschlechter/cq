using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Xml
    {
        public bool? attribute;
        public string name;

        public string @namespace;

        public string prefix;

        public bool? wrapped;
    }
}