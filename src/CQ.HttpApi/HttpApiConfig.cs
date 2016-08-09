using System.Reflection.Emit;
using CQ.HttpApi.ExceptionHandling;
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
            ExceptionHandler = new DefaultExceptionHandler();
        }

        public IExceptionHandler ExceptionHandler { get; set; }

        public IJsonSerializer JsonSerializer { get; set; }

        public IRouteResolver CommandRouteResolver { get; set; }

        public IRouteResolver QueryRouteResolver { get; set; }
    }
}