using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.PathResolving;

namespace CQ.HttpApi
{
    public class HttpApiSettings
    {
        public static readonly HttpApiSettings Default = new HttpApiSettings
        {
            PathResolver = new SimplePathResolver(),
            JsonSerializer = new SimpleJsonSerializer()
        };

        public IJsonSerializer JsonSerializer { get; set; }

        public IPathResolver PathResolver { get; set; }
    }
}