using System;

namespace CQ.HttpApi.RouteResolving
{
    public class DefaultQueryRouteResolver : IRouteResolver
    {
        public string ResolveRoutePath(Type type)
        {
            return type == null ? null : $"Queries/{type.Name}";
        }
    }
}