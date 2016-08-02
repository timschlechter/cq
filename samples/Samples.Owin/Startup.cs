using System.Reflection;
using Business;
using CQ;
using CQ.HttpApi.Owin;
using Owin;
using Business.CommandHandlers;
using Business.QueryHandlers;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Sample.Owin.SelfHost
{
    internal class Startup
    {
        private static readonly Assembly[] CommandAssemblies = {typeof (PlaceOrderCommandHandler).Assembly};
        private static readonly Assembly[] QueryAssemblies = {typeof (GetOrderByIdQueryHandler).Assembly};

        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureContainer(app);

            app.UseCQ(cfg =>
            {
                cfg.EnableCommandHandling(container.GetKnownCommandTypes(), container.DelegateCommandToHandler);
                cfg.EnableQueryHandling(container.GetKnownQueryTypes(), container.DelegateQueryToHandler);
            });
        }

        protected Container ConfigureContainer(IAppBuilder app)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.Register(typeof (ICommandHandler<>), CommandAssemblies);
            container.Register(typeof (IQueryHandler<,>), QueryAssemblies);
            container.Register<IQueryProcessor, ContainerQueryProcessor>();

            container.RegisterSingleton<SamplesStorage>();

            container.Verify();

            app.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            return container;
        }
    }
}