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
    public class CQAppBuilder : IAppBuilder
    {
        private readonly IAppBuilder _app;
        private readonly HttpApiSettings _settings;

        private Type[] _knownCommands;
        private Type[] _knownQueries;

        public CQAppBuilder(IAppBuilder app, HttpApiSettings settings)
        {
            _app = app;
            _settings = settings ?? HttpApiSettings.Default;
        }

        public IAppBuilder Use(object middleware, params object[] args) => _app.Use(middleware, args);

        public object Build(Type returnType) => _app.Build(returnType);

        public IAppBuilder New() => _app.New();

        public IDictionary<string, object> Properties => _app.Properties;

        public CQAppBuilder EnableCommandHandling(IEnumerable<Type> knownCommands, Action<object> handleCommand)
        {
            _knownCommands = (knownCommands ?? Enumerable.Empty<Type>()).ToArray();
            _app.Use(async (context, next) =>
            {
                var commandType = GetCommandType(context);
                var command = _settings.JsonSerializer.Deserialize(context.Request.Body, commandType);

                handleCommand(command);

                await next();
            });
            return this;
        }

        public CQAppBuilder EnableQueryHandling(IEnumerable<Type> knownQueries, Func<object, object> handleQuery)
        {
            _knownQueries = (knownQueries ?? Enumerable.Empty<Type>()).ToArray();
            _app.Use(async (context, next) =>
            {
                var queryType = GetQueryType(context);

                var parameters = context.Request.Query
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.FirstOrDefault());
                dynamic eo = ToExpandObject(parameters);

                using (var stream = new MemoryStream())
                {
                    _settings.JsonSerializer.Serialize(eo, stream);

                    stream.Position = 0;

                    var query = _settings.JsonSerializer.Deserialize(stream, queryType);

                    var result = handleQuery(query);

                    context.Response.ContentType = "application/json";
                    _settings.JsonSerializer.Serialize(result, context.Response.Body);
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

            return _settings.PathResolver.FindCommandTypeByPath(path, _knownCommands);
        }

        private Type GetQueryType(IOwinContext context)
        {
            var path = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;

            return _settings.PathResolver.FindQueryTypeByPath(path, _knownQueries);
        }
    }
}