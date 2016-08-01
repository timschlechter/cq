using CQ.HttpApi.Grouping;
using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.RouteResolving;

namespace CQ.HttpApi
{
    public class HttpApiConfig
    {
        public static readonly HttpApiConfig Default = new HttpApiConfig();

        public HttpApiConfig()
        {
            CommandRouteResolver = new SimpleCommandRouteResolver();
            QueryRouteResolver = new SimpleQueryRouteResolver();

            CommandGroupKeyResolver = new SimpleCommandGroupKeyResolver();
            QueryGroupKeyResolver = new SimpleQueryGroupKeyResolver();

            JsonSerializer = new SimpleJsonSerializer();
        }

        public IGroupKeyResolver QueryGroupKeyResolver { get; set; }

        public IGroupKeyResolver CommandGroupKeyResolver { get; set; }

        public IJsonSerializer JsonSerializer { get; set; }

        public IRouteResolver CommandRouteResolver { get; set; }

        public IRouteResolver QueryRouteResolver { get; set; }
    }
}