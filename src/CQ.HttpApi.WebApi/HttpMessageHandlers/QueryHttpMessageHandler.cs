using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CQ.HttpApi.WebApi.HttpMessageHandlers
{
    internal class QueryHttpMessageHandler : HttpMessageHandler
    {
        private readonly Type _queryType;
        private readonly Func<object, object> _handleQuery;

        public QueryHttpMessageHandler(Type queryType, Func<object, object> handleQuery)
        {
            _queryType = queryType;
            _handleQuery = handleQuery;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = _handleQuery(null);

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}