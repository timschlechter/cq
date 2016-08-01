using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using CQ.HttpApi.RouteResolving;
using Microsoft.Owin;
using Owin;

namespace CQ.HttpApi.Owin
{
    public class OwinConfig : HttpApiConfig
    {
        private readonly IAppBuilder _app;

        public OwinConfig(IAppBuilder app)
        {
            _app = app;
        }

        public Type[] CommandTypes { get; private set; }
        public Type[] QueryTypes { get; private set; }

        public OwinConfig EnableCommandHandling(IEnumerable<Type> commandTypes, Action<object> handleCommand)
        {
            CommandTypes = (commandTypes ?? Enumerable.Empty<Type>()).ToArray();
            _app.Use(async (context, next) =>
            {
                var commandType = GetCommandType(context);

                if (commandType != null)
                {
                    var command = JsonSerializer.Deserialize(context.Request.Body, commandType);

                    handleCommand(command);
                }

                await next();
            });
            return this;
        }

        public OwinConfig EnableQueryHandling(IEnumerable<Type> queryTypes, Func<object, object> handleQuery)
        {
            QueryTypes = (queryTypes ?? Enumerable.Empty<Type>()).ToArray();
            _app.Use(async (context, next) =>
            {
                var queryType = GetQueryType(context);

                if (queryType != null)
                {
                    var parameters = context.Request.Query
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.FirstOrDefault());
                    dynamic eo = ToExpandObject(parameters);

                    using (var stream = new MemoryStream())
                    {
                        JsonSerializer.Serialize(eo, stream);

                        stream.Position = 0;

                        var query = JsonSerializer.Deserialize(stream, queryType);

                        var result = handleQuery(query);

                        context.Response.ContentType = "application/json";
                        JsonSerializer.Serialize(result, context.Response.Body);
                    }
                }
                await next();
            });
            return this;
        }

        public static dynamic ToExpandObject(IDictionary<string, string> valueCollection)
        {
            var result = new ExpandoObject() as IDictionary<string, object>;
            foreach (var kvp in valueCollection)
            {
                result.Add(kvp.Key, kvp.Value);
            }
            return result;
        }

        private Type GetCommandType(IOwinContext context)
        {
            var path = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;

            return CommandTypes.FindTypeByPath(path, CommandRouteResolver);
        }

        private Type GetQueryType(IOwinContext context)
        {
            var path = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;

            return QueryTypes.FindTypeByPath(path, QueryRouteResolver);
        }
    }
}