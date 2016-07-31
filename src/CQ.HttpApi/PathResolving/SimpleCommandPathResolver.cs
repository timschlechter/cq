using System;

namespace CQ.HttpApi.PathResolving
{
    public class SimpleCommandPathResolver : IPathResolver
    {
        public string ResolvePath(Type type)
        {
            return type == null ? null : $"/Commands/{type.Name}";
        }
    }
}