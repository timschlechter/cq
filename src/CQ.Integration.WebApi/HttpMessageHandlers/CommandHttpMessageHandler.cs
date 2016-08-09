using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CQ.HttpApi.JsonSerialization;

namespace CQ.Integration.WebApi.HttpMessageHandlers
{
    public class CommandHttpMessageHandler : DelegatingHandler
    {
        private readonly Type _commandType;
        private readonly Action<object> _handleCommand;
        private readonly IJsonSerializer _serializer;

        public 
            CommandHttpMessageHandler(Type commandType, Action<object> handleCommand, IJsonSerializer serializer)
        {
            _commandType = commandType;
            _handleCommand = handleCommand;
            _serializer = serializer;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.GetDependencyScope();

            var stream = await request.Content.ReadAsStreamAsync();
            var command = _serializer.Deserialize(stream, _commandType);
            _handleCommand(command);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}