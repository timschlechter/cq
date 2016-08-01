using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CQ.HttpApi.WebApi.HttpMessageHandlers
{
    public class CommandHttpMessageHandler : DelegatingHandler
    {
        private Type _commandType;
        private readonly Action<object> _handleCommand;

        public CommandHttpMessageHandler(Type commandType, Action<object> handleCommand)
        {
            _commandType = commandType;
            _handleCommand = handleCommand;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _handleCommand(null);

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}