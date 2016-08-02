using System;
using System.Runtime.Serialization;

namespace Business
{
    [Serializable]
    public class BusinessException : ApplicationException
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BusinessException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}