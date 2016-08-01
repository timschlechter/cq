﻿using System;
using System.Linq;

namespace CQ
{
    public static class Èxtensions
    {
        public static Type GetCommandType(this Type commandHandlerType)
        {
            return commandHandlerType.GetGenericArguments().FirstOrDefault();
        }

        public static Type GetQueryType(this Type queryHandlerType)
        {
            return queryHandlerType.GetGenericArguments().FirstOrDefault();
        }

        public static Type GetResultType(this Type queryType)
        {
            if (!queryType.IsInterface)
            {
                queryType = queryType.GetInterfaces().First();
            }

            return queryType.GetGenericArguments().FirstOrDefault();
        }
    }
}