using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQ.HttpApi;
using CQ.HttpApi.JsonSerialization;

namespace CQ.Integration.WebApi.HttpMessageHandlers
{
    internal class QueryHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<object, object> _handleQuery;
        private readonly Type _queryType;
        private readonly IJsonSerializer _serializer;

        public QueryHttpMessageHandler(Type queryType, Func<object, object> handleQuery, IJsonSerializer serializer)
        {
            _queryType = queryType;
            _handleQuery = handleQuery;
            _serializer = serializer;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.GetDependencyScope();

            var expanded = ObjectHelper.ExpandQueryString(request.RequestUri.Query);
            var query = _serializer.MakeTyped(expanded, _queryType);
            var result = _handleQuery(query);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_serializer.Serialize(result), Encoding.UTF8, "application/json")
            };

            return Task.FromResult(response);
        }
    }
}