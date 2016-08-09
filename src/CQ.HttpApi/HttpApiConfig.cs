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
            CommandRouteResolver = new SimpleCommandRouteResolver();
            QueryRouteResolver = new SimpleQueryRouteResolver();
            JsonSerializer = new SimpleJsonSerializer();
            ExceptionHandler = new SimpleExceptionHandler();
        }

        public IExceptionHandler ExceptionHandler { get; set; }

        public IJsonSerializer JsonSerializer { get; set; }

        public IRouteResolver CommandRouteResolver { get; set; }

        public IRouteResolver QueryRouteResolver { get; set; }
    }
}