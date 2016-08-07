using CQ.HttpApi.JsonSerialization;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace CQ.HttpApi.Client
{
    internal class RestSharpJsonSerializer : ISerializer, IDeserializer
    {
        private readonly IJsonSerializer _serializer;

        public RestSharpJsonSerializer(IJsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public string Serialize(object obj)
        {
            return _serializer.Serialize(obj);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return _serializer.Deserialize<T>(response.Content);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }
    }
}