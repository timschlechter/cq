using System;
using System.Net;

namespace CQ.HttpApi.HttpStatusCodeResolving
{
    public class DefaultHttpStatusCodeResolver : IHttpStatusCodeResolver
    {
        public HttpStatusCode Resolve(Exception ex)
        {
            return HttpStatusCode.InternalServerError;
        }
    }
}