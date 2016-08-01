using System;
using System.Web.Http;

namespace CQ.HttpApi.WebApi
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