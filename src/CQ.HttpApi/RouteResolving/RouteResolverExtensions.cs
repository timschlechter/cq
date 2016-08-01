using System;
using System.Collections.Generic;
using System.Linq;

namespace CQ.HttpApi.RouteResolving
{
    public static class RouteResolverExtensions
    {
        public static string ResolvePath(this IRouteResolver routeResolver, object command)
        {
            return routeResolver.ResolveRoutePath(command?.GetType());
        }

        public static Type FindTypeByPath(this IEnumerable<Type> types, string path, IRouteResolver routeResolver)
        {
            if (types == null || routeResolver == null)
            {
                return null;
            }

            return types.FirstOrDefault(type => PathsAreEqual(path, routeResolver.ResolveRoutePath(type)));
        }

        private static bool PathsAreEqual(string first, string second)
        {
            return string.Equals(first, second, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}