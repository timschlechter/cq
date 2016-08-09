using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQ.HttpApi;
using CQ.HttpApi.HttpStatusCodeResolving;
using CQ.HttpApi.JsonSerialization;

namespace CQ.Integration.WebApi.HttpMessageHandlers
{
    public class CommandHttpMessageHandler : DelegatingHandler
    {
        private readonly Type _commandType;
        private readonly Action<object> _handleCommand;
        private readonly IHttpStatusCodeResolver _httpStatusCodeResolver;
        private readonly IJsonSerializer _serializer;

        public
            CommandHttpMessageHandler(Type commandType, Action<object> handleCommand, IJsonSerializer serializer, IHttpStatusCodeResolver httpStatusCodeResolver)
        {
            _commandType = commandType;
            _handleCommand = handleCommand;
            _serializer = serializer;
            _httpStatusCodeResolver = httpStatusCodeResolver;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            try
            {
                request.GetDependencyScope();

                var stream = await request.Content.ReadAsStreamAsync();
                var command = _serializer.Deserialize(stream, _commandType);
                _handleCommand(command);

                response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var statusCode = _httpStatusCodeResolver.Resolve(ex);

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    StatusCode = statusCode,
                    Content = new StringContent(_serializer.Serialize(new Error {Code = statusCode.ToString(), Message = ex.Message}), Encoding.UTF8, "application/json")
                };
            }

            return response;
        }
    }
}