using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.PathResolving;

namespace CQ.HttpApi
{
    public class HttpApiSettings
    {
        public static readonly HttpApiSettings Default = new HttpApiSettings
        {
            CommandPathResolver = new SimpleCommandPathResolver(),
            QueryPathResolver = new SimpleQueryPathResolver(),
            JsonSerializer = new SimpleJsonSerializer()
        };

        public IJsonSerializer JsonSerializer { get; set; }

        public IPathResolver CommandPathResolver { get; set; }

        public IPathResolver QueryPathResolver { get; set; }
    }
}