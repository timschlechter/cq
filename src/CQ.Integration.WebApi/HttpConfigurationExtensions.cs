using System;
using System.Web.Http;

namespace CQ.Integration.WebApi
{
    public static class HttpConfigurationExtensions
    {
        public static HttpConfiguration UseCQ(this HttpConfiguration config, Action<WebApiConfig> cfg)
        {
            cfg(new WebApiConfig(config));

            return config;
        }
    }
}