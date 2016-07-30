using System;
using System.Collections.Generic;
using System.Linq;

namespace CQ.HttpApi.PathResolving
{
    public static class PathResolverExtensions
    {
        public static string GetCommandPath(this IPathResolver pathResolver, object command)
        {
            return pathResolver.GetCommandPath(command?.GetType());
        }

        public static string GetQueryPath(this IPathResolver pathResolver, object query)
        {
            return pathResolver.GetQueryPath(query?.GetType());
        }

        public static Type FindCommandTypeByPath(this IPathResolver pathResolver, string path, IEnumerable<Type> commandTypes)
        {
            if (commandTypes == null || pathResolver == null)
            {
                return null;
            }

            return commandTypes.FirstOrDefault(type => PathsAreEqual(path, pathResolver.GetCommandPath(type)));
        }

        public static Type FindQueryTypeByPath(this IPathResolver pathResolver, string path, IEnumerable<Type> queryTypes)
        {
            if (queryTypes == null || pathResolver == null)
            {
                return null;
            }

            return queryTypes.FirstOrDefault(type => PathsAreEqual(path, pathResolver.GetQueryPath(type)));
        }

        private static bool PathsAreEqual(string first, string second)
        {
            return string.Equals(first, second, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}