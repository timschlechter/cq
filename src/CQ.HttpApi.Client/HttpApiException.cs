using System;
using System.Net;
using System.Runtime.Serialization;

namespace CQ.Client
{
    [Serializable]
    public class HttpApiException : Exception
    {
        public HttpApiException()
        {
        }

        public HttpApiException(string message) : base(message)
        {
        }

        public HttpApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HttpApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}