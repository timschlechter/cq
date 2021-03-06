using System.Reflection;
using Business;
using Business.CommandHandlers;
using Business.QueryHandlers;
using CQ;
using CQ.CommandHandlerDecorators;
using CQ.Integration.Owin;
using CQ.QueryHandlerDecorators;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Sample.Owin.SelfHost
{
    internal class Startup
    {
        private static readonly Assembly[] CommandAssemblies = {typeof(PlaceOrderCommandHandler).Assembly};
        private static readonly Assembly[] QueryAssemblies = {typeof(GetOrderByIdQueryHandler).Assembly};

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

            container.Register(typeof(ICommandHandler<>), CommandAssemblies);
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionCommandHandlerDecorator<>));

            container.Register(typeof(IQueryHandler<,>), QueryAssemblies);
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ValidationQueryHandlerDecorator<,>));

            container.RegisterSingleton<SampleStorage>();

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