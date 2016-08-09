using System;
using System.Net;

namespace CQ.HttpApi.HttpStatusCodeResolving
{
    public interface IHttpStatusCodeResolver
    {
        HttpStatusCode Resolve(Exception ex);
    }
}