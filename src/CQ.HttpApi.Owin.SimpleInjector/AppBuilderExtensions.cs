using System;
using System.Linq;
using Owin;
using SimpleInjector;

namespace CQ.HttpApi.Owin.SimpleInjector
{
    public static class AppBuilderExtensions
    {
        public static CQAppBuilderDecorator UseCQ(this IAppBuilder app, Container container, HttpApiSettings settings = null)
        {
            return app.UseCQ(settings)
                .EnableCommandHandling(container)
                .EnableQueryHandling(container);
        }

        private static CQAppBuilderDecorator EnableCommandHandling(this CQAppBuilderDecorator app, Container container)
        {
            var commandTypes = container.GetKnownCommandTypes().ToArray();

            if (!commandTypes.Any())
            {
                return app;
            }

            var commandExecutors = container.GetKnownCommandHandlerTypes()
                .ToDictionary(
                    commandHandlerType => commandHandlerType.GetCommandType(),
                    commandHandlerType =>
                    {
                        var commandType = commandHandlerType.GetCommandType();
                        var methodInfo = commandHandlerType.GetMethod("Handle");
                        var handleMethodInfo = methodInfo.MakeGenericMethod(commandType);

                        Action<object> handle = command =>
                        {
                            var commandHandler = container.GetCommandHandler(command);
                            handleMethodInfo.Invoke(commandHandler, new[] {command});
                        };

                        return handle;
                    });

            return app.EnableCommandHandling(commandTypes, command =>
            {
                if (command == null)
                {
                    throw new HttpApiException("Command is null");
                }

                var commandType = command.GetType();
                if (!commandExecutors.ContainsKey(commandType))
                {
                    throw new HttpApiException($"No handler registered for commands of type {commandType.AssemblyQualifiedName}");
                }

                commandExecutors[commandType](command);
            });
        }

        private static CQAppBuilderDecorator EnableQueryHandling(this CQAppBuilderDecorator app, Container container)
        {
            var queryTypes = container.GetKnownQueryTypes().ToArray();

            if (!queryTypes.Any())
            {
                return app;
            }

            var queryExecutors = container.GetKnownQueryHandlerTypes()
                .ToDictionary(
                    queryHandlerType => queryHandlerType.GetQueryType(),
                    queryHandlerType =>
                    {
                        var queryType = queryHandlerType.GetQueryType();
                        var methodInfo = queryHandlerType.GetMethod("Handle");
                        var handleMethodInfo = methodInfo.MakeGenericMethod(queryType);

                        Func<object, object> handle = query =>
                        {
                            var queryHandler = container.GetQueryHandler(query);
                            return handleMethodInfo.Invoke(queryHandler, new[] {query});
                        };

                        return handle;
                    });

            return app.EnableQueryHandling(queryTypes, query =>
            {
                if (query == null)
                {
                    throw new HttpApiException("Query is null");
                }

                var queryType = query.GetType();
                if (!queryExecutors.ContainsKey(queryType))
                {
                    throw new HttpApiException($"No handler registered for queries of type {queryType.AssemblyQualifiedName}");
                }
                return queryExecutors[queryType](query);
            });
        }
    }
}