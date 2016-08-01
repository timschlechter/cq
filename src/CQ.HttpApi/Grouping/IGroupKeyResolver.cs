using System;

namespace CQ.HttpApi.Grouping
{
    public interface IGroupKeyResolver
    {
        string ResolveGroupKey(Type type);
    }
}