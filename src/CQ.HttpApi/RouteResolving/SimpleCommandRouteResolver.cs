using System;

namespace CQ.HttpApi.RouteResolving
{
    public class SimpleCommandRouteResolver : IRouteResolver
    {
        public string ResolveRoutePath(Type type)
        {
            return type == null ? null : $"Commands/{type.Name}";
        }
    }
}