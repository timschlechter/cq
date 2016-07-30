using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace CQ.HttpApi.Owin.SimpleInjector
{
    internal static class ContainerExtensions
    {
        public static IEnumerable<Type> GetCommandTypes(this Container container)
        {
            return container.GetRegisteredCommandHandlerTypes()
                .Select(type => type.GetGenericArguments().Single());
        }

        public static IEnumerable<Type> GetQueryTypes(this Container container)
        {
            return container.GetRegisteredQueryHandlerTypes()
                .Select(type => type.GetGenericArguments().First());
        }

        public static IEnumerable<Type> GetRegisteredCommandHandlerTypes(this Container container)
        {
            return container.GetCurrentRegistrations()
                .Select(instanceProducer => instanceProducer.ServiceType)
                .Where(serviceType => serviceType.IsOpenGenericType(typeof (IHandleCommand<>)));
        }

        public static IEnumerable<Type> GetRegisteredQueryHandlerTypes(this Container container)
        {
            return container.GetCurrentRegistrations()
                .Select(instanceProducer => instanceProducer.ServiceType)
                .Where(serviceType => serviceType.IsOpenGenericType(typeof (IHandleQuery<,>)));
        }

        private static bool IsOpenGenericType(this Type type, Type openGeneric)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == openGeneric;
        }
    }
}