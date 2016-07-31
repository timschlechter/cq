using System;
using CQ.Client;
using CQ.HttpApi.Owin;
using Microsoft.Owin.Hosting;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    public abstract class OwinBaseTests
    {
        private readonly string RootUrl = "http://localhost:12345";
        private IDisposable _app;

        private HttpApiClient _httpApiClient { get; set; }


        [SetUp]
        public virtual void SetUp()
        {
            _app = WebApp.Start(RootUrl, app => Configure(app.UseCQ()));

            _httpApiClient = new HttpApiClient(RootUrl);
        }

        protected abstract void Configure(CQAppBuilderDecorator app);


        [TearDown]
        public void Cleanup()
        {
            if (_app != null)
            {
                _app.Dispose();
                _app = null;
            }

            if (_httpApiClient != null)
            {
                _httpApiClient = null;
            }
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