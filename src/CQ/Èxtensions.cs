using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return queryType.GetGenericArguments().Skip(1).FirstOrDefault();
        }
    }
}
