using System.Diagnostics.CodeAnalysis;

namespace CQ.HttpApi.Owin.Swagger.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Contact
    {
        public string name;

        public string url;

        public string email;
    }
}