using System;
using System.Collections.Generic;
using System.Linq;
using CQ.HttpApi;
using CQ.HttpApi.JsonSerialization;
using CQ.HttpApi.RouteResolving;
using Microsoft.Owin;
using Owin;

namespace CQ.Integration.Owin
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
                if (context.Request.Method == "POST")
                {
                    var commandType = GetCommandType(context);

                    if (commandType != null)
                    {
                        Handle(context, () =>
                        {
                            var command = JsonSerializer.Deserialize(context.Request.Body, commandType);

                            handleCommand(command);

                            context.Response.StatusCode = 200;
                            JsonSerializer.Serialize("", context.Response.Body);
                        });
                    }
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
                if (context.Request.Method == "GET")
                {
                    var queryType = GetQueryType(context);

                    if (queryType != null)
                    {
                        Handle(context, () =>
                        {
                            var eo = ObjectHelper.ExpandQueryString(context.Request.QueryString.Value);

                            var query = JsonSerializer.MakeTyped(eo, queryType);
                            var result = handleQuery(query);
                            context.Response.ContentType = "application/json";
                            JsonSerializer.Serialize(result, context.Response.Body);
                        });
                    }
                }
                await next();
            });
            return this;
        }

        private void Handle(IOwinContext context, Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var statusCode = HttpStatusCodeResolver.Resolve(ex);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                JsonSerializer.Serialize(new Error { Code = statusCode.ToString(), Message = ex.Message }, context.Response.Body);
            }
        }

        private Type GetCommandType(IOwinContext context)
        {
            var path = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }
            return CommandTypes.FindTypeByPath(path, CommandRouteResolver);
        }

        private Type GetQueryType(IOwinContext context)
        {
            var path = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }
            return QueryTypes.FindTypeByPath(path, QueryRouteResolver);
        }
    }
}