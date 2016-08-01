using System;
using CQ.Client;
using CQ.HttpApi.Owin;
using Microsoft.Owin.Hosting;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    public abstract class OwinBaseTests
    {
        [ThreadStatic] private static IDisposable _app;
        [ThreadStatic] private static HttpApiClient _httpApiClient;

        private readonly string RootUrl = "http://localhost:12345";

        [SetUp]
        public virtual void SetUp()
        {
            _app = WebApp.Start(RootUrl, app => app.UseCQ(Configure));

            _httpApiClient = new HttpApiClient(RootUrl);
        }

        protected abstract void Configure(OwinConfig cfg);


        [TearDown]
        public void Cleanup()
        {
            if (_app != null)
            {
                _app.Dispose();
                _app = null;
            }

            _httpApiClient = null;
        }


        protected void ExecuteCommand<TCommand>(TCommand command)
        {
            _httpApiClient.ExecuteCommand(command).Wait(250);
        }

        protected TResult ExecuteQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var task = _httpApiClient.ExecuteQuery<TQuery, TResult>(query);
                
            task.Wait(250);

            return task.Result;
        }
    }
}