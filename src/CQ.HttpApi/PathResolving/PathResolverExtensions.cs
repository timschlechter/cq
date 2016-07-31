using System;
using System.Collections.Generic;
using System.Linq;

namespace CQ.HttpApi.PathResolving
{
    public static class PathResolverExtensions
    {
        public static string ResolvePath(this IPathResolver pathResolver, object command)
        {
            return pathResolver.ResolvePath(command?.GetType());
        }
        public static Type FindTypeByPath(this IEnumerable<Type> types, string path, IPathResolver pathResolver)
        {
            if (types == null || pathResolver == null)
            {
                return null;
            }

            return types.FirstOrDefault(type => PathsAreEqual(path, pathResolver.ResolvePath(type)));
        }

        private static bool PathsAreEqual(string first, string second)
        {
            return string.Equals(first, second, StringComparison.CurrentCultureIgnoreCase);
        }

    }
}