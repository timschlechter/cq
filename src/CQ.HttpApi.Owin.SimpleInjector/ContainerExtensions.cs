using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace CQ.HttpApi.Owin.SimpleInjector
{
    internal static class ContainerExtensions
    {
        public static object GetCommandHandler(this Container container, object command)
        {
            var commandType = command.GetType();
            var commandHandlerType = typeof(IHandleCommand<>).MakeGenericType(commandType);
            return container.GetInstance(commandHandlerType);
        }

        public static object GetQueryHandler(this Container container, object query)
        {
            var queryType = query.GetType();
            var resultType = query.GetType().GetResultType();
            var queryHandlerType = typeof(IHandleQuery<,>).MakeGenericType(queryType, resultType);
            return container.GetInstance(queryHandlerType);
        }
        public static IEnumerable<Type> GetKnownCommandTypes(this Container container)
        {
            return container.GetCommandHandlerRegistrations()
                .Select(instanceProducer => instanceProducer.ServiceType.GetGenericArguments().Single());
        }

        public static IEnumerable<Type> GetKnownQueryTypes(this Container container)
        {
            return container.GetQueryHandlerRegistrations()
                 .Select(instanceProducer => instanceProducer.ServiceType.GetGenericArguments().Single());
        }

        public static IEnumerable<Type> GetKnownCommandHandlerTypes(this Container container)
        {
            return container.GetCommandHandlerRegistrations().Select(ip => ip.ServiceType);
        }

        public static IEnumerable<Type> GetKnownQueryHandlerTypes(this Container container)
        {
            return container.GetQueryHandlerRegistrations().Select(ip => ip.ServiceType);
        }

        public static IEnumerable<InstanceProducer> GetCommandHandlerRegistrations(this Container container)
        {
            return container.GetCurrentRegistrations()
                .Where(instanceProducer => instanceProducer.ServiceType.IsOpenGenericType(typeof (IHandleCommand<>)));
        }

        public static IEnumerable<InstanceProducer> GetQueryHandlerRegistrations(this Container container)
        {
            return container.GetCurrentRegistrations()
                .Where(instanceProducer => instanceProducer.ServiceType.IsOpenGenericType(typeof(IHandleQuery<,>)));
        }

        private static bool IsOpenGenericType(this Type type, Type openGeneric)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == openGeneric;
        }
    }
}