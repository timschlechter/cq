using System.Reflection.Emit;
using CQ.HttpApi.HttpStatusCodeResolving;
using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.RouteResolving;

namespace CQ.HttpApi
{
    public class HttpApiConfig
    {
        public HttpApiConfig()
        {
            CommandRouteResolver = new DefaultCommandRouteResolver();
            QueryRouteResolver = new DefaultQueryRouteResolver();
            JsonSerializer = new DefaultJsonSerializer();
            HttpStatusCodeResolver = new DefaultHttpStatusCodeResolver();
        }

        public IHttpStatusCodeResolver HttpStatusCodeResolver { get; set; }

        public IJsonSerializer JsonSerializer { get; set; }

        public IRouteResolver CommandRouteResolver { get; set; }

        public IRouteResolver QueryRouteResolver { get; set; }
    }
}