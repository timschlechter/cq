using System;

namespace CQ.HttpApi.PathResolving
{
    public interface IPathResolver
    {
        string ResolvePath(Type type);
    }
}