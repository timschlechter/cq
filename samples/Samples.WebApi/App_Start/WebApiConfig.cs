﻿using System;
using System.Reflection;
using System.Web.Http;
using Business.CommandHandlers;
using Business.QueryHandlers;
using CQ;
using CQ.HttpApi.WebApi;
using Samples.WebApi.Code;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Swashbuckle.Application;

namespace Samples.WebApi
{
    public class WebApiConfig
    {
        private static readonly Assembly[] CommandAssemblies = {typeof (CreateOrderCommandHandler).Assembly};
        private static readonly Assembly[] QueryAssemblies = {typeof (GetOrderByIdQueryHandler).Assembly};

        public static void Configure(HttpConfiguration config)
        {
            var container = ConfigureContainer();

            config.UseCQ(cfg =>
            {
                var customRouteResolver = new CustomRouteResolver();
                cfg.CommandRouteResolver = customRouteResolver;
                cfg.QueryRouteResolver = customRouteResolver;

                cfg.EnableCommandHandling(container.GetKnownCommandTypes(), container.DelegateCommandToHandler);
                cfg.EnableQueryHandling(container.GetKnownQueryTypes(), container.DelegateQueryToHandler);
            });

            GlobalConfiguration.Configuration
                .EnableSwagger(cfg =>
                {
                    cfg.SingleApiVersion("v1", "Samples.WebApi");
                    cfg.GroupActionsBy(apiDescription =>
                    {
                        var result = apiDescription.RelativePath.Replace("Api/", "");
                        return result.Substring(0, result.IndexOf("/", StringComparison.InvariantCulture));
                    });
                })
                .EnableSwaggerUi();
        }

        private static Container ConfigureContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register(typeof (IHandleCommand<>), CommandAssemblies);
            container.Register(typeof (IHandleQuery<,>), QueryAssemblies);

            container.Verify();

            return container;
        }
    }
}