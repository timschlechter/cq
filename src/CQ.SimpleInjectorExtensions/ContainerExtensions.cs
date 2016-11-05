using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace CQ
{
    public static class ContainerExtensions
    {
        public static void DelegateCommandToHandler(this Container container, object command)
        {
            var handlerFunction = container.ResolveCommandHandlerAction(command);

            handlerFunction(command);
        }

        public static object DelegateQueryToHandler(this Container container, object query)
        {
            var handlerFunction = container.ResolveQueryHandlerFunction(query);

            return handlerFunction(query);
        }

        /// <summary>
        ///     Returns an Action which invokes <see cref="ICommandHandler{TCommand}.Handle" />
        ///     of the commandhandler registered for handling the given <paramref name="command" />
        /// </summary>
        public static Action<object> ResolveCommandHandlerAction(this Container container, object command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var commandType = command.GetType();
            var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
            dynamic commandHandler = container.GetInstance(commandHandlerType);

            return c => commandHandler.Handle((dynamic) c);
        }

        /// <summary>
        ///     Returns a Func which invokes <see cref="IQueryHandlerQueryHandler{TQuery,TResult}.Handle" />
        ///     of the queryhandler registered for handling the given <paramref name="query" />
        /// </summary>
        public static Func<object, object> ResolveQueryHandlerFunction(this Container container, object query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var queryType = query.GetType();

            var resultType = queryType
                .GetInterfaces().Single()
                .GetGenericArguments().Single();

            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, resultType);
            dynamic queryHandler = container.GetInstance(queryHandlerType);

            return q => queryHandler.Handle((dynamic) q);
        }

        public static IEnumerable<Type> GetKnownCommandTypes(this Container container)
        {
            return container.GetCommandHandlerRegistrations()
                .Select(instanceProducer => instanceProducer.ServiceType.GetGenericArguments().FirstOrDefault());
        }

        public static IEnumerable<Type> GetKnownQueryTypes(this Container container)
        {
            return container.GetQueryHandlerRegistrations()
                .Select(instanceProducer => instanceProducer.ServiceType.GetGenericArguments().FirstOrDefault());
        }

        public static IEnumerable<InstanceProducer> GetCommandHandlerRegistrations(this Container container)
        {
            return container.GetCurrentRegistrations()
                .Where(instanceProducer => instanceProducer.ServiceType.ImplementsOpenGeneric(typeof(ICommandHandler<>)));
        }

        public static IEnumerable<InstanceProducer> GetQueryHandlerRegistrations(this Container container)
        {
            return container.GetCurrentRegistrations()
                .Where(instanceProducer => instanceProducer.ServiceType.ImplementsOpenGeneric(typeof(IQueryHandler<,>)));
        }

        private static bool ImplementsOpenGeneric(this Type type, Type openGenericType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == openGenericType;
        }
    }
}