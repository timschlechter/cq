using System;

namespace CQ.HttpApi.RouteResolving
{
    public interface IRouteResolver
    {
        string ResolveRoutePath(Type type);
    }
}