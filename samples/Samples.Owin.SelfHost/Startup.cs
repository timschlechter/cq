using System.Reflection;
using CQ;
using CQ.HttpApi.Owin.SimpleInjector;
using CQ.HttpApi.Owin.Swagger;
using Owin;
using Samples.Business.CommandHandlers;
using Samples.Business.QueryHandlers;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Sample.Owin.SelfHost
{
    internal class Startup
    {
        private static readonly Assembly[] CommandAssemblies = {typeof (CreateOrderCommandHandler).Assembly};
        private static readonly Assembly[] QueryAssemblies = { typeof(GetOrderByIdQueryHandler).Assembly };

        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureContainer(app);

            app.UseCQ(container)
                .EnableSwagger();
        }

        protected Container ConfigureContainer(IAppBuilder app)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            container.Register(typeof (IHandleCommand<>), CommandAssemblies);
            container.Register(typeof (IHandleQuery<,>), QueryAssemblies);

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