using System;

namespace CQ.HttpApi.RouteResolving
{
    public class SimpleQueryRouteResolver : IRouteResolver
    {
        public string ResolveRoutePath(Type type)
        {
            return type == null ? null : $"Queries/{type.Name}";
        }
    }
}