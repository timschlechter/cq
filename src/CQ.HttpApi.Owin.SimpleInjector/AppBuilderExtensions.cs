using System.Linq;
using Owin;
using SimpleInjector;

namespace CQ.HttpApi.Owin.SimpleInjector
{
    public static class AppBuilderExtensions
    {
        public static CQAppBuilderDecorator UseCQ(this IAppBuilder app, Container container, HttpApiSettings settings = null)
        {
            var commandTypes = container.GetCommandTypes();
            var queryTypes = container.GetQueryTypes();

            return app.UseCQ(settings)
                .EnableCommandHandling(commandTypes, command =>
                {
                    var commandType = command.GetType();
                    var commandHandlerType = typeof (IHandleCommand<>).MakeGenericType(commandType);
                    var methodInfo = commandHandlerType.GetMethod("Handle");
                    var handleMethodInfo = methodInfo.MakeGenericMethod(commandType);
                    var commandHandler = container.GetInstance(commandHandlerType);
                    handleMethodInfo.Invoke(commandHandler, new[] {command});
                })
                .EnableQueryHandling(queryTypes, query =>
                {
                    var queryType = query.GetType();
                    var resultType = queryType.GetGenericArguments().Skip(1).Single();
                    var queryHandlerType = typeof (IHandleQuery<,>).MakeGenericType(queryType, resultType);
                    var methodInfo = queryHandlerType.GetMethod("Handle");
                    var handleMethodInfo = methodInfo.MakeGenericMethod(queryType);
                    var quryHandler = container.GetInstance(queryHandlerType);
                    return handleMethodInfo.Invoke(quryHandler, new[] {query});
                });
        }
    }
}