using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace CQ
{
    public static class ContainerExtensions
    {
        /// <summary>
        ///     Returns an Action which invokes <see cref="IHandleCommand{TCommand}.Handle" />
        ///     of the commandhandler registered for handling the given <paramref name="command" />
        /// </summary>
        public static Action<object> ResolveCommandHandlerAction(this Container container, object command)
        {
            var commandType = command.GetType();
            var commandHandlerType = typeof (IHandleCommand<>).MakeGenericType(commandType);
            var commandHandler = container.GetInstance(commandHandlerType);
            var handleMethodInfo = commandHandler.GetType().GetMethod("Handle");
            return c => handleMethodInfo.Invoke(commandHandler, new[] {command});
        }

        /// <summary>
        ///     Returns an Func which invokes <see cref="IHandleQuery{TQuery, TResult}.Handle" />
        ///     of the queryhandler registered for handling the given <paramref name="query" />
        /// </summary>
        public static Func<object, object> ResolveQueryHandlerFunction(this Container container, object query)
        {
            var queryType = query.GetType();
            var resultType = query.GetType().GetResultType();
            var queryHandlerType = typeof (IHandleQuery<,>).MakeGenericType(queryType, resultType);
            var queryHandler = container.GetInstance(queryHandlerType);
            var handleMethodInfo = queryHandler.GetType().GetMethod("Handle");
            return q => handleMethodInfo.Invoke(queryHandler, new[] {q});
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
                .Where(instanceProducer => instanceProducer.ServiceType.ImplementsOpenGeneric(typeof (IHandleCommand<>)));
        }

        public static IEnumerable<InstanceProducer> GetQueryHandlerRegistrations(this Container container)
        {
            return container.GetCurrentRegistrations()
                .Where(instanceProducer => instanceProducer.ServiceType.ImplementsOpenGeneric(typeof (IHandleQuery<,>)));
        }

        private static bool ImplementsOpenGeneric(this Type type, Type openGenericType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == openGenericType;
        }
    }
}