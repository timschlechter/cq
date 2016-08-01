using System;

namespace CQ.HttpApi.Grouping
{
    public class SimpleQueryGroupKeyResolver : IGroupKeyResolver
    {
        public string ResolveGroupKey(Type type) => "Queries";
    }
}