using System;
using CQ.HttpApi.Grouping;

namespace Samples.WebApi.Code
{
    /// <summary>
    ///     Simple resolver used to demonstrate custom group key resolving
    /// </summary>
    public class CustomGroupKeyResolver : IGroupKeyResolver
    {
        public string ResolveGroupKey(Type type)
        {
            var result = type.FullName
                .Replace("Contracts.Commands.", "")
                .Replace("Contracts.Queries.", "");

            return result.Substring(0, result.IndexOf(".", StringComparison.InvariantCulture));
        }
    }
}