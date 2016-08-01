using System;
using CQ.HttpApi.RouteResolving;

namespace Samples.WebApi.Code
{
    /// <summary>
    /// Simple resolver used to demonstrate custom path resolving
    /// </summary>
    public class CustomRouteResolver : IRouteResolver
    {
        public string ResolveRoutePath(Type type)
        {
            return "Api/" + type.FullName.Replace(".", "/")
                .Replace("Contracts/", "")
                .Replace("Queries/", "")
                .Replace("Commands/", "")
                .Replace("Query", "")
                .Replace("Command", "");
        }
    }
}