using System;

namespace CQ.HttpApi.PathResolving
{
    public class SimplePathResolver : IPathResolver
    {
        public string GetCommandPath(Type commandType)
        {
            return commandType == null ? null : $"/Commands/{commandType.Name}";
        }

        public string GetQueryPath(Type queryPath)
        {
            return queryPath == null ? null : $"/Queries/{queryPath.Name}";
        }
    }
}