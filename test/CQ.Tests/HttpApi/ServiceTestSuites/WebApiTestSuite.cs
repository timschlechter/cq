using System;
using System.Web.Http;
using CQ.HttpApi.JsonSerialization;
using CQ.Integration.WebApi;
using Microsoft.Owin.Hosting;
using NUnit.Framework;
using Owin;

namespace CQ.HttpApi.Tests.HttpApi.ServiceTestSuites
{
    [TestFixture]
    public class WebApiTestSuite : ServiceTestSuite
    {
        private static IDisposable _app;

        protected override void StartService(string rootUrl, Type[] commandTypes, Action<object> handleCommand, Type[] queryTypes, Func<object, object> handleQuery)
        {
            _app = WebApp.Start(rootUrl, app =>
            {
                var configuration = new HttpConfiguration();

                app.UseWebApi(configuration);

                configuration.UseCQ(cfg =>
                {
                    cfg.JsonSerializer = new SimpleJsonSerializer();
                    cfg.EnableCommandHandling(commandTypes, handleCommand);
                    cfg.EnableQueryHandling(queryTypes, handleQuery);
                });
            });
        }

        protected override void StopService()
        {
            if (_app != null)
            {
                _app.Dispose();
                _app = null;
            }
        }
    }
}