using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.RouteResolving;

namespace CQ.HttpApi
{
    public class HttpApiSettings
    {
        public static readonly HttpApiSettings Default = new HttpApiSettings
        {
            CommandRouteResolver = new SimpleCommandRouteResolver(),
            QueryRouteResolver = new SimpleQueryRouteResolver(),
            JsonSerializer = new SimpleJsonSerializer()
        };

        public IJsonSerializer JsonSerializer { get; set; }

        public IRouteResolver CommandRouteResolver { get; set; }

        public IRouteResolver QueryRouteResolver { get; set; }
    }
}