using System;

namespace CQ.HttpApi.PathResolving
{
    public class SimpleQueryPathResolver : IPathResolver
    {
        public string ResolvePath(Type type)
        {
            return type == null ? null : $"/Queries/{type.Name}";
        }
    }
}