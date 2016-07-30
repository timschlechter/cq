using System;

namespace CQ.HttpApi.PathResolving
{
    public interface IPathResolver
    {
        string GetCommandPath(Type commandType);
        string GetQueryPath(Type queryPath);
    }
}