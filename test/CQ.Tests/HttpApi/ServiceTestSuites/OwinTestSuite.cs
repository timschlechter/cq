using System;
using CQ.HttpApi.Owin;
using Microsoft.Owin.Hosting;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.ServiceTestSuites
{
    [TestFixture]
    public class OwinTestSuite : ServiceTestSuite
    {
        private static IDisposable _app;

        protected override void StartService(string rootUrl, Type[] commandTypes, Action<object> handleCommand, Type[] queryTypes, Func<object, object> handleQuery)
        {
            _app = WebApp.Start(rootUrl, app => app.UseCQ(cfg =>
            {
                cfg.EnableCommandHandling(commandTypes, handleCommand);
                cfg.EnableQueryHandling(queryTypes, handleQuery);
            }));
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