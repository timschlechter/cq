using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using CQ.HttpApi.PathResolving;
using Microsoft.Owin;
using Owin;

namespace CQ.HttpApi.Owin
{
    public class CQAppBuilderDecorator : IAppBuilder
    {
        private Type[] _commandTypes;
        private Type[] _queryTypes;

        public CQAppBuilderDecorator(IAppBuilder decoratedApp, HttpApiSettings settings)
        {
            DecoratedApp = decoratedApp;
            Settings = settings ?? HttpApiSettings.Default;
        }

        public IAppBuilder DecoratedApp { get; }

        public HttpApiSettings Settings { get; }

        public IAppBuilder Use(object middleware, params object[] args) => DecoratedApp.Use(middleware, args);

        public object Build(Type returnType) => DecoratedApp.Build(returnType);

        public IAppBuilder New() => DecoratedApp.New();

        public IDictionary<string, object> Properties => DecoratedApp.Properties;

        public CQAppBuilderDecorator EnableCommandHandling(IEnumerable<Type> commandTypes, Action<object> handleCommand)
        {
            _commandTypes = (commandTypes ?? Enumerable.Empty<Type>()).ToArray();
            DecoratedApp.Use(async (context, next) =>
            {
                var commandType = GetCommandType(context);
                var command = Settings.JsonSerializer.Deserialize(context.Request.Body, commandType);

                handleCommand(command);

                await next();
            });
            return this;
        }

        public CQAppBuilderDecorator EnableQueryHandling(IEnumerable<Type> queryTypes, Func<object, object> handleQuery)
        {
            _queryTypes = (queryTypes ?? Enumerable.Empty<Type>()).ToArray();
            DecoratedApp.Use(async (context, next) =>
            {
                var queryType = GetQueryType(context);

                var parameters = context.Request.Query
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.FirstOrDefault());
                dynamic eo = ToExpandObject(parameters);

                using (var stream = new MemoryStream())
                {
                    Settings.JsonSerializer.Serialize(eo, stream);

                    stream.Position = 0;

                    var query = Settings.JsonSerializer.Deserialize(stream, queryType);

                    var result = handleQuery(query);

                    context.Response.ContentType = "application/json";
                    Settings.JsonSerializer.Serialize(result, context.Response.Body);
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

            return _commandTypes.FindTypeByPath(path, Settings.CommandPathResolver);
        }

        private Type GetQueryType(IOwinContext context)
        {
            var path = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;

            return _queryTypes.FindTypeByPath(path, Settings.QueryPathResolver);
        }
    }
}