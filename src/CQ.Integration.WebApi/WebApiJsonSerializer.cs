using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using CQ.HttpApi.JsonSerialization;

namespace CQ.Integration.WebApi
{
    public class WebApiJsonSerializer : IJsonSerializer
    {
        private readonly HttpConfiguration _httpConfiguration;

        public WebApiJsonSerializer(HttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration;
        }

        private JsonMediaTypeFormatter JsonFormatter => _httpConfiguration.Formatters.JsonFormatter;

        public object Deserialize(Stream stream, Type type)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return JsonFormatter.CreateJsonSerializer().Deserialize(reader, type);
            }
        }

        public void Serialize(object value, Stream stream)
        {
            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
            {
                JsonFormatter.CreateJsonSerializer().Serialize(writer, value);
            }
        }
    }
}