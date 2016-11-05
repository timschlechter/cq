using System;
using System.Reflection;
using System.Web.Http;
using Business;
using Business.CommandHandlers;
using Business.QueryHandlers;
using CQ;
using CQ.CommandHandlerDecorators;
using CQ.Integration.WebApi;
using CQ.QueryHandlerDecorators;
using Samples.WebApi.Code;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Swashbuckle.Application;

namespace Samples.WebApi
{
    public class WebApiConfig
    {
        private static readonly Assembly[] CommandAssemblies = {typeof(PlaceOrderCommandHandler).Assembly};
        private static readonly Assembly[] QueryAssemblies = {typeof(GetOrderByIdQueryHandler).Assembly};

        public static void Configure(HttpConfiguration config)
        {
            var container = ConfigureContainer();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            config.UseCQ(cfg =>
            {
                var customRouteResolver = new CustomRouteResolver();
                cfg.CommandRouteResolver = customRouteResolver;
                cfg.QueryRouteResolver = customRouteResolver;

                cfg.EnableCommandHandling(container.GetKnownCommandTypes(), container.DelegateCommandToHandler);
                cfg.EnableQueryHandling(container.GetKnownQueryTypes(), container.DelegateQueryToHandler);
            });

            config.EnableSwagger(cfg =>
            {
                cfg.SingleApiVersion("v1", "Samples.WebApi");
                cfg.GroupActionsBy(apiDescription =>
                {
                    var result = apiDescription.RelativePath.Replace("Api/", "");
                    return result.Substring(0, result.IndexOf("/", StringComparison.InvariantCulture));
                });

                cfg.SchemaFilter<CustomSchemaFilter>();
                cfg.OperationFilter<CustomOperationFilter>();
            }).EnableSwaggerUi();
        }

        private static Container ConfigureContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register(typeof(ICommandHandler<>), CommandAssemblies);
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionCommandHandlerDecorator<>));

            container.Register(typeof(IQueryHandler<,>), QueryAssemblies);
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ValidationQueryHandlerDecorator<,>));

            container.RegisterSingleton<SampleStorage>();

            container.Verify();

            return container;
        }
    }
}