using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Info
    {
        public Contact contact;

        public string description;

        public License license;

        public string termsOfService;

        public string title;

        public Dictionary<string, object> vendorExtensions = new Dictionary<string, object>();
        public string version;
    }
}