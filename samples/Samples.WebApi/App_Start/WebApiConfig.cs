using System.Reflection;
using System.Web.Http;
using CQ;
using CQ.HttpApi.WebApi;
using Business.CommandHandlers;
using Business.QueryHandlers;
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

                var customGroupKeyResolver = new CustomGroupKeyResolver();
                cfg.CommandGroupKeyResolver = customGroupKeyResolver;
                cfg.QueryGroupKeyResolver = customGroupKeyResolver;
                
                cfg.EnableCommandHandling(container.GetKnownCommandTypes(), container.ExecuteCommand);
                cfg.EnableQueryHandling(container.GetKnownQueryTypes(), container.ExecuteQuery);
            });

            GlobalConfiguration.Configuration
                .EnableSwagger(c => c.SingleApiVersion("v1", "Samples.WebApi"))
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