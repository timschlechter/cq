using System;

namespace CQ.HttpApi.Grouping
{
    public class SimpleCommandGroupKeyResolver : IGroupKeyResolver
    {
        public string ResolveGroupKey(Type type) => "Commands";
    }
}