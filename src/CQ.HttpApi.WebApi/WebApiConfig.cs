using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using CQ.HttpApi.Grouping;
using CQ.HttpApi.WebApi.ActionDescriptors;
using CQ.HttpApi.WebApi.HttpMessageHandlers;

namespace CQ.HttpApi.WebApi
{
    public class WebApiConfig : HttpApiConfig
    {
        private readonly HttpConfiguration _httpConfiguration;

        public WebApiConfig(HttpConfiguration httpConfiguration)
        {
            _httpConfiguration = httpConfiguration;
        }

        public WebApiConfig EnableCommandHandling(IEnumerable<Type> commandTypes, Action<object> handleCommand)
        {
            commandTypes = commandTypes ?? Enumerable.Empty<Type>();

            foreach (var commandType in commandTypes)
            {
                var route = RegisterCommandHandlerRoute(commandType, handleCommand);

                RegisterCommandHandlerApiDescription(commandType, route);
            }

            return this;
        }

        public WebApiConfig EnableQueryHandling(IEnumerable<Type> queryTypes, Func<object, object> handleQuery)
        {
            queryTypes = queryTypes ?? Enumerable.Empty<Type>();

            foreach (var queryType in queryTypes)
            {
                var route = RegisterQueryHandlerRoute(queryType, handleQuery);

                RegisterQueryHandlerApiDescription(queryType, route);
            }

            return this;
        }
        
        protected virtual IHttpRoute RegisterCommandHandlerRoute(Type commandType, Action<object> handleCommand)
        {
            var routePath = CommandRouteResolver.ResolveRoutePath(commandType);
            var routeTemplate = ToValidRouteTemplate(routePath);

            return _httpConfiguration.Routes.MapHttpRoute(
                commandType.AssemblyQualifiedName,
                routeTemplate,
                new {},
                new {},
                new CommandHttpMessageHandler(commandType, handleCommand));
        }

        protected virtual IHttpRoute RegisterQueryHandlerRoute(Type queryType, Func<object, object> handleQuery)
        {
            var routePath = QueryRouteResolver.ResolveRoutePath(queryType);
            var routeTemplate = ToValidRouteTemplate(routePath);

            return _httpConfiguration.Routes.MapHttpRoute(
                queryType.AssemblyQualifiedName,
                routeTemplate,
                new {},
                new {},
                new QueryHttpMessageHandler(queryType, handleQuery));
        }

        protected virtual void RegisterCommandHandlerApiDescription(Type commandType, IHttpRoute route)
        {
            var apiExplorer = _httpConfiguration.Services.GetApiExplorer();
            var groupKey = CommandGroupKeyResolver.ResolveGroupKey(commandType);

            var apiDescription = new ApiDescription
            {
                Route = route,
                HttpMethod = HttpMethod.Post,
                RelativePath = route.RouteTemplate,
                ActionDescriptor = new CommandActionDescriptor(_httpConfiguration, commandType, groupKey)
            };

            var paramInfo = GetType().GetMethod("ParamInfoDummy", BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(commandType)
                .GetParameters()[0];

            apiDescription.ParameterDescriptions.Add(new ApiParameterDescription
            {
                Source = ApiParameterSource.FromBody,
                ParameterDescriptor = new ReflectedHttpParameterDescriptor(apiDescription.ActionDescriptor, paramInfo)
            });

            apiExplorer.ApiDescriptions.Add(apiDescription);
        }

        protected virtual void RegisterQueryHandlerApiDescription(Type queryType, IHttpRoute route)
        {
            var apiExplorer = _httpConfiguration.Services.GetApiExplorer();
            var groupKey = QueryGroupKeyResolver.ResolveGroupKey(queryType);

            var apiDescription = new ApiDescription
            {
                Route = route,
                HttpMethod = HttpMethod.Get,
                RelativePath = route.RouteTemplate,
                ActionDescriptor = new QueryActionDescriptor(_httpConfiguration, queryType, groupKey)
            };

            

            var paramInfo = GetType().GetMethod("ParamInfoDummy", BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(queryType)
                .GetParameters()[0];

            apiDescription.ParameterDescriptions.Add(new ApiParameterDescription
            {
                Source = ApiParameterSource.FromUri,
                ParameterDescriptor = new ReflectedHttpParameterDescriptor(apiDescription.ActionDescriptor, paramInfo)
            });

            apiExplorer.ApiDescriptions.Add(apiDescription);
        }

        private static string ToValidRouteTemplate(string routePath)
        {
            return routePath != null && routePath.StartsWith("/") ? routePath.Substring(1) : routePath;
        }

        private void ParamInfoDummy<T>(T command)
        {
            // HACK: Dummy method used to retrieve ParameterInfo for a generic parameter
        }
    }
}